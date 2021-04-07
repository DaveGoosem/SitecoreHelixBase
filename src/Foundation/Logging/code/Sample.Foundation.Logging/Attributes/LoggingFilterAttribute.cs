using Sample.Foundation.Logging.Services;
using System;
using System.Web.Mvc;
using System.Web;
using System.Text;
using System.Diagnostics;

namespace Sample.Foundation.Logging.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private const string StopwatchKey = "DebugLoggingStopWatch";
        static ILogService _logService = new LogService();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_logService.IsDebug())
            {
                var requestUrl = HttpContext.Current.Request.Url.ToString();
                var httpReferer = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                var loggingWatch = Stopwatch.StartNew();
                filterContext.HttpContext.Items.Add(StopwatchKey, loggingWatch);

                var message = new StringBuilder();
                message.Append(string.Format("Executing controller : {0}, action : {1}, Requested URL : {2}, Http Referer : {3} ",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName, requestUrl, httpReferer));

                _logService.Info(message.ToString());
            }
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (_logService.IsDebug())
            {
                if (filterContext.HttpContext.Items[StopwatchKey] != null)
                {
                    var requestUrl = HttpContext.Current.Request.Url.ToString();
                    var httpReferer = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                    var loggingWatch = (Stopwatch)filterContext.HttpContext.Items[StopwatchKey];
                    loggingWatch.Stop();

                    long timeSpent = loggingWatch.ElapsedMilliseconds;

                    var message = new StringBuilder();
                    message.Append(string.Format("Finished executing controller {0}, action {1},  Requested URL : {2}, Http Referer : {3}  - time spent {4}",
                        filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        filterContext.ActionDescriptor.ActionName, requestUrl, httpReferer,
                        timeSpent));

                    _logService.Info(message.ToString());
                    filterContext.HttpContext.Items.Remove(StopwatchKey);
                }
            }
        }
    }
}