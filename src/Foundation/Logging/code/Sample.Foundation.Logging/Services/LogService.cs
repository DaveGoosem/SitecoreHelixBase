using Sample.Foundation.DependencyInjection;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using System;

namespace Sample.Foundation.Logging.Services
{
    [Service(typeof(ILogService))]
    public class LogService : ILogService
    {
        public LogService()
        {
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        public void Error(string message, Exception exception)
        {
            Log.Error(message, exception, this);
        }

        public void Error(string message)
        {
            Log.Error(message, this);
        }

        public void Fatal(string message, Exception exception)
        {
            Log.Fatal(message, exception, this);
        }

        public void Fatal(string message)
        {
            Log.Fatal(message, this);
        }

        public void Info(string message)
        {
            Log.Info(message, this);
        }

        public void DebugInfo(string message)
        {
            if (IsDebug())
            {
                Log.Info(message, this);
            }
        }

        public void Warn(string message)
        {
            Log.Warn(message, this);
        }

        public void Warn(string message, Exception exception)
        {
            Log.Warn(message, exception, this);
        }

        /// <summary>
        /// Checks for debug flag in config to enable debug logging.
        /// Typcially used for logging sensitive information or developer only detail.
        /// Defaults to false for safety.
        /// </summary>        
        public bool IsDebug()
        {
            var configValue = Settings.GetSetting("LoggingDebugModeEnabled");
            if (string.IsNullOrEmpty(configValue))
                return false;

            try
            {
                return Convert.ToBoolean(configValue);
            }
            catch (Exception ex)
            {
                Error("There was a problem retrieving the web.config value for LoggingDebugModeEnabled or converting it to bool. Values must be 'true' or 'false'", ex);
                return false;
            }
        }
    }
}