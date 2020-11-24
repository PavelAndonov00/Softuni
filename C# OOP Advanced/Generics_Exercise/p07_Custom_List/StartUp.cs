namespace p08_Custom_List_Sorter
{
    using Contracts;
    using System;

    class StartUp
    {
        static void Main(string[] args)
        {
            ICustomList<string> customList = new CustomList<string>();

            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = tokens[0];
                switch (command)
                {
                    case "Add":
                        customList.Add(tokens[1]);
                        break;
                    case "Remove":
                        customList.Remove(int.Parse(tokens[1]));
                        break;
                    case "Contains":
                        Console.WriteLine(customList.Contains(tokens[1]));
                        break;
                    case "Swap":
                        customList.Swap(int.Parse(tokens[1]), int.Parse(tokens[2]));
                        break;
                    case "Greater":
                        Console.WriteLine(customList.CountGreaterThan(tokens[1]));
                        break;
                    case "Max":
                        Console.WriteLine(customList.Max());
                        break;
                    case "Min":
                        Console.WriteLine(customList.Min());
                        break;
                    case "Print":
                        Console.WriteLine(customList);
                        break;
                }

                input = Console.ReadLine();
            }
        }       
    }
}
