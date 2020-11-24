namespace Logger.Core
{
    using Logger.Appenders.Contracts;
    using Logger.Appenders.Factory;
    using Logger.Appenders.Factory.Contracts;
    using Logger.Core.Contracts;
    using Logger.Layouts.Contracts;
    using Logger.Layouts.Factory;
    using Logger.Layouts.Factory.Contracts;
    using Logger.Loggers.Contracts;
    using System.Collections.Generic;
    using Logger.Loggers;
    using Logger.Loggers.Factory.Contracts;
    using Logger.Loggers.Factory;
    using Logger.Enumeration;
    using System;
    using System.Text;
    using System.Linq;

    public class CommandInterpreter : ICommandInterpreter
    {
        private ILayoutFactory layoutFactory;
        private IAppenderFactory appenderFactory;
        private ILoggerFactory loggerFactory;
        private ICollection<IAppender> appenders;

        public CommandInterpreter()
        {
            this.layoutFactory = new LayoutFactory();
            this.appenderFactory = new AppenderFactory();
            this.loggerFactory = new LoggerFactory();
            this.appenders = new List<IAppender>();
        }

        public void AddAppender(string[] args)
        {
            string appenderType = args[0];
            string layoutType = args[1];
            string reportLevel = null;
            if (args.Length == 3)
            {
                reportLevel = args[2];
            }

            ILayout layout = layoutFactory.CreateLayout(layoutType);          

            IAppender appender = appenderFactory.CreateAppender(appenderType, layout, reportLevel);

            appenders.Add(appender);
        }

        public void AddMessage(string[] args)
        {
            ReportLevel reportLevel = Enum.Parse<ReportLevel>(args[0].First().ToString().ToUpper() + args[0].Substring(1).ToLower());
            string time = args[1];
            string message = args[2];

            foreach (var appender in appenders)
            {
                appender.Append(time, reportLevel, message);
            }
        }

        public string GetStatistics()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IAppender appender in appenders)
            {
                stringBuilder.AppendLine(appender.ToString());
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
