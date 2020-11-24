namespace P02_BlackBoxInteger
{
    using System;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            var classType = typeof(BlackBoxInteger);
            var classInstance = (BlackBoxInteger)Activator.CreateInstance(classType, true);

            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] tokens = input
                    .Split("_", StringSplitOptions.RemoveEmptyEntries);

                var method = classType
                    .GetMethod(tokens[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .Invoke(classInstance, new object[] { int.Parse(tokens[1]) });

                int value = (int)classType.GetField("innerValue", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
                    .GetValue(classInstance);
                Console.WriteLine(value);

                input = Console.ReadLine();
            }


        }
    }
}
