namespace Logger.Appenders
{
    using Logger.Appenders.Contracts;
    using Logger.Enumeration;
    using Logger.Layouts.Contracts;
    using Logger.Loggers;
    using Logger.Loggers.Contracts;
    using System.IO;

    public class FileAppender : IAppender
    {
        private const string path = "../../../log.txt";
        private readonly ILayout layout;
        private readonly ILogFile logFile;

        public FileAppender(ILayout layout, ILogFile logFile)
        {
            this.layout = layout;
            this.logFile = logFile;
        }

        public ReportLevel ReportLevel { get; set; }
        public int AppendedMessages { get; private set; }

        public void Append(string datetime, ReportLevel reportLevel, string errorMessage)
        {
            if (this.ReportLevel <= reportLevel)
            {
                string content = string.Format(this.layout.Format, datetime, reportLevel.ToString().ToUpper(), errorMessage);
                File.AppendAllText(path, content + "\n");
                logFile.Write(content);

                AppendedMessages++;
            }

        }

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {layout.GetType().Name}, Report level: {ReportLevel.ToString().ToUpper()}, Messages appended: {AppendedMessages}, File size: {logFile.Size}";
        }
    }
}
