namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];
            inputParameters = inputParameters.Skip(1).ToList();

            string result = string.Empty;

            var method = tankManager
                .GetType()
                .GetMethods()
                .FirstOrDefault(m => m.Name.Contains(command));

            result = (string)method.Invoke(tankManager, new object[] { inputParameters });

            return result;
        }
    }
}