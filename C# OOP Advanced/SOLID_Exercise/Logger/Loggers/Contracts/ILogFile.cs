namespace Logger.Loggers.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ILogFile
    {
        void Write(string message);

        int Size { get; }
    }
}
