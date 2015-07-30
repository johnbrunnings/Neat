using NLog;

namespace Neat.Infrastructure.Logging
{
    public class NLogProvider : ILogProvider
    {
        public void LogInfo(string message, LoggerContextInfo loggerContextInfo)
        {
            var loggerName = loggerContextInfo.ObjectType.FullName + "." + loggerContextInfo.MethodName;
            var logger = LogManager.GetLogger(loggerName);
            logger.Info(message);
        }

        public void LogWarning(string message, LoggerContextInfo loggerContextInfo)
        {
            var loggerName = loggerContextInfo.ObjectType.FullName + "." + loggerContextInfo.MethodName;
            var logger = LogManager.GetLogger(loggerName);
            logger.Warn(message);
        }

        public void LogError(string message, LoggerContextInfo loggerContextInfo)
        {
            var loggerName = loggerContextInfo.ObjectType.FullName + "." + loggerContextInfo.MethodName;
            var logger = LogManager.GetLogger(loggerName);
            logger.Error(message);
        }
    }
}