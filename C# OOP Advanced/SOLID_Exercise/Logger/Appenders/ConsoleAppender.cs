namespace Logger.Appenders
{
    using System;
    using Contracts;
    using Logger.Enumeration;
    using Logger.Layouts.Contracts;

    public class ConsoleAppender : IAppender
    {
        private readonly ILayout layout;

        public ConsoleAppender(ILayout layout)
        {
            this.layout = layout;
        }

        public ReportLevel ReportLevel { get; set; }
        public int AppendedMessages { get; private set; }

        public void Append(string datetime, ReportLevel reportLevel, string errorMessage)
        {
            if(this.ReportLevel <= reportLevel)
            {
                Console.WriteLine(String.Format(layout.Format, datetime, reportLevel.ToString().ToUpper(), errorMessage));

                AppendedMessages++;
            }       
        }

        public override string ToString()
        {
            return $"Appender type: {this.GetType().Name}, Layout type: {layout.GetType().Name}, Report level: {ReportLevel.ToString().ToUpper()}, Messages appended: {AppendedMessages}";
        }
    }
}
