using System;
using System.Linq;

namespace p01_ListyIterator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IListyIterator<string> listyIterator = null;
            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                try
                {
                    switch (command)
                    {
                        case "Create":
                            listyIterator = new ListyIterator<string>(tokens.Skip(1).ToList());
                            break;
                        case "HasNext":
                            Console.WriteLine(listyIterator.HasNext());
                            break;
                        case "Move":
                            Console.WriteLine(listyIterator.Move());
                            break;
                        case "Print":
                            listyIterator.Print();
                            break;
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }

                input = Console.ReadLine();
            }
        }
    }
}
