using log4net;
using log4net.Config;

namespace Project
{
    public static class LoggerHandler
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LoggerHandler));

        public static void InitLog4net()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("/home/coder/project/workspace/Project/log4net.config"));
        }

        public static void LogDebug(string message)
        {
            log.Debug(message);
        }

        public static void LogInfo(string message)
        {
            log.Info(message);
        }

        public static void LogWarn(string message)
        {
            log.Warn(message);
        }

        public static void LogError(string message)
        {
            log.Error(message);
        }

        public static void LogFatal(string message)
        {
            log.Fatal(message);
        }
    }
}
