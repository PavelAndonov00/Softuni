namespace Logger.Loggers.Factory.Contracts
{
    using global::Logger.Appenders.Contracts;
    using global::Logger.Loggers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILoggerFactory
    {
        ILogger CreateLogger(ICollection<IAppender> appenders);
    }
}
