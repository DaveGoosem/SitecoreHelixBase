var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var foreach = require("gulp-foreach");
var rename = require("gulp-rename");
var watch = require("gulp-watch");
var newer = require("gulp-newer");
var util = require("gulp-util");
var path = require("path");
var rimrafDir = require("rimraf");
var rimraf = require("gulp-rimraf");
var config = require("./gulp-config.js")();
var nugetRestore = require('gulp-nuget-restore');
var fs = require('fs');
var unicorn = require("./scripts/unicorn.js");
var project = require("./scripts/project.js");
var yargs = require("yargs").argv;

module.exports.config = config;

gulp.task("publish", callback => {
    config.runCleanBuilds = true;
    return gulp.series([
        "Publish-All-Projects",
        "Publish-All-Configs",
        "Publish-All-Views"])
        (callback);
});

/*****************************
  Initial setup
*****************************/
gulp.task("Copy-Sitecore-License", function () {
    console.log("Copying Sitecore License file");

    return gulp.src(config.licensePath).pipe(gulp.dest("./lib"));
});

gulp.task("Nuget-Restore", function (callback) {
    var solution = "./" + config.solutionName + ".sln";
    return gulp.src(solution).pipe(nugetRestore());
});

gulp.task("Publish-All-Projects", function (callback) {
    // this publishes the project to the file system
    // msbuild automatically handles project dependencies
    // as long as a VS project is added to the publishAllProject , its assembly and dependencies will makes it to the publish folder too.
    var commandParams = [
        config.publishAllProject,
        `/tv:${config.buildToolsVersion}`,
        `/verbosity:${config.buildVerbosity}`,
        `/target:WebPublish`,
        `/p:Configuration=${config.buildConfiguration}`,
        `/p:Platform=${config.publishPlatform}`,
        `/p:WebPublishMethod=FileSystem`,
        `/p:publishUrl=${config.publishRoot}`];


    const spawn = require('child_process').spawn;
    const msbuildProcess = spawn(config.msbuildbat, commandParams, { shell: true });

    msbuildProcess.stdout.on('data', (data) => {
        console.log(data.toString());
    });

    msbuildProcess.stderr.on('data', (data) => {
        console.log(data.toString());
    });

    msbuildProcess.on('exit', (code) => {
        if (code !== 0) {
            callback(`msbuild failed with code ${code}`);  // error
            return;
        }

        callback(null); // success
    });
});

gulp.task("Sync-Unicorn", function (callback) {
    var options = {};
    options.siteHostName = project.getSiteUrl();
    options.authenticationConfigFile = config.publishRoot + "/App_config/Include/Unicorn/Unicorn.UI.config";

    unicorn(function () { return callback() }, options);
});

gulp.task("Publish-All-Views", function () {
    var root = "./src";
    var roots = [root + "/**/code/**/Views", "!" + root + "/**/obj/**/Views"];
    var files = "/**/*.cshtml";

    var destination = config.publishRoot + "\\Views";

    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Publish-All-Configs", function () {
    var root = "./src";
    var roots = [root + "/**/App_Config", "!" + root + "/**/obj/**/App_Config", "!" + root + "/**/**/code/CIBuild/App_Config"];
    var files = "/**/*.{config,config.disabled}";    

    var destination = config.publishRoot + "\\App_Config";
    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});


/*****************************
 CI Tasks
*****************************/

//See also: https://github.com/gulpjs/gulp/issues/2116 for some good examples
gulp.task("CI-Build", callback => {    
    config.publishRoot = path.resolve(__dirname, "CIBuild");  // it may be better to read from CI $(Build.SourcesDirectory) environment variable instead of using __dirname in the event gulpfile.js gets moved
    config.buildConfiguration = "Release";
    config.ciMode = true;

    return gulp.series([
        "Package-Clean",
        "Publish-All-Projects",
        "Delete-Vanilla-Sitecore-Dlls-from-publish-root",
        "Publish-All-Configs",
        "Publish-All-Views",
        "Publish-All-Css",
        "Publish-All-Js",
        "Publish-All-Fonts",
        "Publish-All-Images",
        "Publish-Custom-Sitecore",
        "CI-Copy-Items",
        "CI-Copy-WebConfigs",
        //"Copy-Web-Config-To-Build-Folder",
        /*"Perform-CI-Cleanup"*/])(callback);
});

/* Copy flattened unicorn items to CI build artifact folder */
gulp.task("CI-Copy-Items", function () {
    var destination = config.publishRoot + "/Unicorn";
    return gulp.src("./src/**/serialization/**/*.yml")
        .pipe(gulp.dest(destination));
});

/* Copy custom Web.configs to CI build artifact folder */
gulp.task("CI-Copy-WebConfigs", function () {
    var destination = config.publishRoot + "/WebConfigs";
    return gulp.src("./src/Project/All/code/**/WebConfigs/*.config.disabled")
        .pipe(gulp.dest(destination));
});

/* Remove temp files */
gulp.task("Package-Clean", function (callback) {
    rimrafDir.sync(path.resolve(config.publishRoot));
    callback();
});

//remove unwanted files by adding to the array (runs at the end of CI Build process)
//gulp.task("Perform-CI-Cleanup", function (callback) {
//    return gulp.src([config.publishRoot + "/App_Config/Include/Project/z.SITENAME.DevSettings.config", config.publishRoot + "/web.CI.config.disabled"], { read: false, allowEmpty: true }) // much faster     
//        .pipe(rimraf());
//});

//gulp.task("Copy-Web-Config-To-Build-Folder", function () {
//    var originalCIConfigFile = "./src/Project/Common/code/web.CI.config.disabled";
//    util.log("copying web.config to the build folder");
//    return gulp.src(originalCIConfigFile)
//        .pipe(gulp.dest(config.publishRoot))
//        .pipe(rename('.\\web.config'))
//        .pipe(gulp.dest(config.publishRoot));
//});

gulp.task("Publish-All-Css", function () {
    var root = "./src";
    var roots = [root + "/**/styles", "!" + root + "/**/obj/**/styles"];
    var files = "/**/*.css*(.map)";
    var destination = config.publishRoot + "\\styles";

    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Publish-All-Images", function () {
    var root = "./src";
    var roots = [root + "/**/code/**/images"];
    var files = "/**/*.{jpg,svg,png,gif,ico}";
    var destination = config.publishRoot + "\\images";

    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Publish-All-Js", function () {
    var root = "./src";
    var roots = [root + "/**/code/**/scripts"];
    var files = "/**/*.js*(.map)";
    var destination = config.publishRoot + "\\scripts";

    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Publish-Custom-Sitecore", function () {
    var root = "./src";
    var roots = [root + "/**/code/**/Sitecore"];
    var files = ["/**/*"];
    var destination = config.publishRoot + "\\sitecore";
    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Publish-All-Fonts", function () {
    var root = "./src";
    var roots = [root + "/**/code/**/styles/fonts"];
    var files = "/**/*.{eot,svg,woff,woff2,ttf,otf}";
    var destination = config.publishRoot + "\\styles\\fonts";

    return gulp.src(roots, { base: root }).pipe(
        foreach(function (stream, file) {
            console.log("Publishing from " + file.path);
            gulp.src(file.path + files, { base: file.path })
                .pipe(newer(destination))
                .pipe(debug({ title: "Copying " }))
                .pipe(gulp.dest(destination));
            return stream;
        })
    );
});

gulp.task("Delete-Vanilla-Sitecore-Dlls-from-publish-root", function () {
    return gulp.src([config.publishRoot + '/bin/Sitecore.*.{dll,pdb}', '!**/Sitecore.Configuration.Roles.dll', '!**/Sitecore.Support.*.{dll,pdb}'], { read: false })
        .pipe(debug({ title: "to delete:" }))
        .pipe(rimraf());
});