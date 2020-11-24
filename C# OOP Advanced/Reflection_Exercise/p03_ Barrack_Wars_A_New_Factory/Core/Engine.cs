namespace _03BarracksFactory.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Contracts;
    using p03__Barrack_Wars_A_New_Factory.Core;

    class Engine : IRunnable
    {
        private IRepository repository;
        private IUnitFactory unitFactory;

        public Engine(IRepository repository, IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }
        
        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string[] data = input.Split();
                    string commandName = data[0];
                    string result = InterpredCommand(data, commandName);
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        // TODO: refactor for Problem 4
        private string InterpredCommand(string[] data, string commandName)
        {
            string name = commandName.Substring(0, 1).ToUpper() + commandName.Substring(1);
            string typeName = "p03__Barrack_Wars_A_New_Factory.Core.Commands." + name + "Command";
            var type = Type.GetType(typeName);
            var instance = (IExecutable)Activator.CreateInstance(type, new object[] { data });
            var fields = type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttributes(false).Any(ca => ca.GetType() == typeof(Injection)));

            foreach (var commandField in fields)
            {
                var engineClassFieldValue = typeof(Engine)
                    .GetField(commandField.Name, BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(this);

                commandField.SetValue(instance, engineClassFieldValue);
            }

            string result = instance.Execute();
            return result;
        }


        private string ReportCommand(string[] data)
        {
            string output = this.repository.Statistics;
            return output;
        }


        private string AddUnitCommand(string[] data)
        {
            string unitType = data[1];
            IUnit unitToAdd = this.unitFactory.CreateUnit(unitType);
            this.repository.AddUnit(unitToAdd);
            string output = unitType + " added!";
            return output;
        }
    }
}
