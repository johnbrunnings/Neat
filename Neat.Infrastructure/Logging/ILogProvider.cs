namespace Neat.Infrastructure.Logging
{
    public interface ILogProvider
    {
        void LogInfo(string message, LoggerContextInfo loggerContextInfo);
        void LogWarning(string message, LoggerContextInfo loggerContextInfo);
        void LogError(string message, LoggerContextInfo loggerContextInfo);
    }
}