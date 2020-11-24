namespace p03_Stack
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] tokens = input.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string command = tokens[0];
                try
                {
                    switch (command)
                    {
                        case "Push":
                            stack.Push(tokens.Skip(1).Select(int.Parse).ToArray());
                            break;
                        case "Pop":
                            stack.Pop();
                            break;
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }


                input = Console.ReadLine();
            }

            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }
    }
}
