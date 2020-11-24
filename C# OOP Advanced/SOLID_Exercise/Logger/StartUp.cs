namespace Logger
{
    using Logger.Appenders;
    using Logger.Appenders.Contracts;
    using Logger.Layouts;
    using Logger.Layouts.Contracts;
    using Logger.Loggers.Contracts;
    using Logger.Loggers;
    using System;
    using Logger.Enumeration;
    using Logger.Core;

    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
