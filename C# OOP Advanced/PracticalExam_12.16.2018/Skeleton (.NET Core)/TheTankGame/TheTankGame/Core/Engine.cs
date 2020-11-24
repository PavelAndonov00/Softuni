namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.isRunning = false;
        }

        public void Run()
        {
            this.isRunning = true;

            while (isRunning)
            {
                var input = (IList<string>)this
               .reader
               .ReadLine()
               .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                this.writer.WriteLine(this.commandInterpreter.ProcessInput(input));

                if(input[0] == "Terminate")
                {
                    isRunning = false;
                }
            }
        }
    }
}