namespace Logger.Appenders.Factory.Contracts
{
    using Logger.Appenders.Contracts;
    using Logger.Layouts.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAppenderFactory
    {
        IAppender CreateAppender(string appenderType, ILayout layout, string reportLevel);
    }
}
