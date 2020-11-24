namespace Logger.Loggers
{
    using global::Logger.Loggers.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LogFile : ILogFile
    {
        public int Size { get; private set; }

        public void Write(string message)
        {
            this.Size += message.Where(Char.IsLetter).Sum(x => x);
        }
    }
}
