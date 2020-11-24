namespace Logger.Appenders.Factory
{
    using Logger.Appenders.Contracts;
    using Logger.Appenders.Factory.Contracts;
    using Logger.Enumeration;
    using Logger.Layouts.Contracts;
    using Logger.Loggers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AppenderFactory : IAppenderFactory
    {
        public IAppender CreateAppender(string appenderType, ILayout layout, string reportLevel)
        {
            IAppender appender = null;
            switch (appenderType)
            {
                case "ConsoleAppender":
                    appender = new ConsoleAppender(layout);
                    break;
                case "FileAppender":
                   appender = new FileAppender(layout, new LogFile());
                    break;                    
            }

            if(appender == null)
            {
                throw new ArgumentException("Invalid appender type!");
            }
            else
            {
                if(reportLevel != null)
                {
                    appender.ReportLevel = Enum.Parse<ReportLevel>(reportLevel.First().ToString().ToUpper() + reportLevel.Substring(1).ToLower());
                }

                return appender;
            }
        }
    }
}
