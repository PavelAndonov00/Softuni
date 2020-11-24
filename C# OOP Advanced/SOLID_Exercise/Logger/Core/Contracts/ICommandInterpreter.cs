namespace Logger.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICommandInterpreter
    {
        void AddAppender(string[] args);

        void AddMessage(string[] args);

        string GetStatistics();
    }
}
