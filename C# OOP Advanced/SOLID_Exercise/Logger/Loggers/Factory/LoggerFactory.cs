namespace Logger.Loggers.Factory
{
    using global::Logger.Appenders;
    using global::Logger.Appenders.Contracts;
    using global::Logger.Loggers.Contracts;
    using global::Logger.Loggers.Factory.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LoggerFactory : ILoggerFactory
    {
        public ILogger CreateLogger(ICollection<IAppender> appenders)
        {
            if(appenders.Count == 2)
            {
                return new Logger(appenders.FirstOrDefault(a => a is ConsoleAppender),
                    appenders.FirstOrDefault(a => a is FileAppender));
            }

            return new Logger(appenders.FirstOrDefault(a => a is ConsoleAppender));
        }
    }
}
