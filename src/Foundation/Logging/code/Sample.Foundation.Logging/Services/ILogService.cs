using System;

namespace Sample.Foundation.Logging.Services
{
    public interface ILogService
    {
        void Info(string message);
        void Error(string message, Exception exception);
        void Error(string message);
        void Warn(string message);
        void Warn(string message, Exception exception);
        void Fatal(string message, Exception exception);
        void Fatal(string message);
        void Debug(string message);
        bool IsDebug();

        /// <summary>
        /// Used for info messages when web.config debug log is set to 'true'. This can be used to log potentially sensitive information etc. 
        /// Regular Debug will still be logged as normal
        /// </summary>
        /// <param name="message"></param>
        void DebugInfo(string message);
    }
}
