namespace Logger.Core
{
    using Logger.Core.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Engine : IEngine
    {
        public void Run()
        {
            ICommandInterpreter commandInterpreter = new CommandInterpreter();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] args = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    commandInterpreter.AddAppender(args);
                }
                catch (ArgumentException ae)
                {               
                    Console.WriteLine(ae.Message);
                }
            }

            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] args = input.Split("|", StringSplitOptions.RemoveEmptyEntries);
                commandInterpreter.AddMessage(args);

                input = Console.ReadLine();
            }

            Console.WriteLine(commandInterpreter.GetStatistics());
        }
    }
}
