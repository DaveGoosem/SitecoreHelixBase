using Sample.Foundation.DependencyInjection;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Data.Items;
using System;

namespace Sample.Foundation.GlassMapper.Services
{
    [Service(typeof(IGlassMapperService))]
    public class GlassMapperService : IGlassMapperService
    {
        /// <summary>
        /// see http://www.glass.lu/mapper/documentation/Upgrade-ToV5 for detail on the v4 to v5 upgrade and why we have this abstraction layer so that we only
        /// have to update the library here and everything in all our helix projects can use our own wrapper implementation of Glass Mapper
        /// </summary>
        private IMvcContext _webMvcContext;
        private ISitecoreService _webSitecoreContext;

        private IMvcContext _masterMvcContext;
        private ISitecoreService _masterSitecoreContext;

        private IMvcContext _coreMvcContext;
        private ISitecoreService _coreSitecoreContext;

        private IMvcContext _currentContext;

        public GlassMapperService()
        {
            _currentContext = new MvcContext();

            _webSitecoreContext = new SitecoreService(Constants.WebContextName);
            _webMvcContext = new MvcContext(_webSitecoreContext);

            _masterSitecoreContext = new SitecoreService(Constants.MasterContextName);
            _masterMvcContext = new MvcContext(_masterSitecoreContext);

            _coreSitecoreContext = new SitecoreService(Constants.CoreContextName);
            _coreMvcContext = new MvcContext(_coreSitecoreContext);
        }

        /// <summary>
        /// Get the Context you would like to use. Values are "web", "master", "core" and stored in the Constants file in the GM Foundation Project
        /// </summary>
        /// <param name="contextName"></param>        
        public IMvcContext GetMvcContext(string contextName = Constants.WebContextName)
        {
            switch (contextName)
            {
                case Constants.MasterContextName:
                    return _masterMvcContext;
                case Constants.CoreContextName:
                    return _coreMvcContext;
                case Constants.WebContextName:
                default:
                    return _webMvcContext;
            }
        }

        public T GetCurrentItem<T>(IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.GetContextItem<T>();
        }

        public T GetDataSourceItem<T>(IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.GetDataSourceItem<T>();
        }

        public T GetItem<T>(string path, IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.SitecoreService.GetItem<T>(path);
        }

        public T GetItem<T>(Guid guid, IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.SitecoreService.GetItem<T>(guid);
        }

        public T Cast<T>(Item item, IMvcContext context) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.SitecoreService.GetItem<T>(item);
        }

        /// <summary>
        /// Writes to the db in whichever Context is supplied
        /// </summary>
        /// <param name="options"></param>
        /// <param name="database"></param>
        public void WriteToItem(WriteToItemOptions options, IMvcContext context = null)
        {
            if (context == null)
                context = _currentContext;

            context.SitecoreService.WriteToItem(options);
        }

        public void SaveItem<T>(T model) where T : class
        {
            var saveOptions = new SaveOptions();
            saveOptions.Model = model;
            saveOptions.UpdateStatistics = true;
            saveOptions.Silent = false;

            _masterMvcContext.SitecoreService.SaveItem(saveOptions);
        }

        /// <summary>
        /// Creates a new Item in Sitecore using the Master DB Context
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>        
        public T CreateItem<T>(CreateByModelOptions options) where T : class
        {
            return _masterMvcContext.SitecoreService.CreateItem<T>(options);
        }

        /// <summary>
        /// Returns the Parent item of the Home/Start item for the site which should be the Tenant top level "Site" item
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        public T GetTenantItem<T>(IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.GetRootItem<T>();
        }

        /// <summary>
        /// Returns the Root item of the site (home item) cast to your preferred type
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        public T GetStartItem<T>(IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            return context.GetHomeItem<T>();
        }

        public void DeleteItem<T>(DeleteByModelOptions options, IMvcContext context = null) where T : class
        {
            if (context == null)
                context = _currentContext;

            context.SitecoreService.DeleteItem(options);
        }
    }
}