namespace Logger.Loggers
{
    using Contracts;
    using global::Logger.Appenders.Contracts;
    using global::Logger.Enumeration;

    public class Logger : ILogger
    {
        private readonly IAppender appender;
        private readonly IAppender fileAppender;

        public Logger(IAppender appender)
        {
            this.appender = appender;
        }


        public Logger(IAppender appender, IAppender fileAppender)
            : this(appender)
        {
            this.fileAppender = fileAppender;
        }

        public void Critical(string dateTime, string criticalMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.Critical, criticalMessage);
        }

        public void Error(string dateTime, string errorMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.Error, errorMessage);
        }

        public void Fatal(string dateTime, string fatalMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.Fatal, fatalMessage);
        }

        public void Info(string dateTime, string infoMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.Info, infoMessage);
        }

        public void Warning(string dateTime, string warningMessage)
        {
            this.AppendMessage(dateTime, ReportLevel.Warning, warningMessage);
        }

        public void AppendMessage(string dateTime, ReportLevel errorLevel, string message)
        {
            appender?.Append(dateTime, errorLevel, message);
            fileAppender?.Append(dateTime, errorLevel, message);
        }
    }
}
