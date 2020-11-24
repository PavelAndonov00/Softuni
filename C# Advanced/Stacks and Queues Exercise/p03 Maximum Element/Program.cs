using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_Maximum_Element
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbersToPush = int.Parse(Console.ReadLine());

            var stack = new Stack<int>();
            for (int i = 0; i < numbersToPush; i++)
            {
                var input = Console.ReadLine()
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                if (input.Length > 1)
                {
                    if (input[0] != 1) return;
                    stack.Push(input[1]);                        
                }
                else
                {
                    switch (input[0])
                    {
                        case 2:
                               stack.Pop();
                            break;
                        case 3:
                            Console.WriteLine(stack.Max());
                            break;
                    }
                }
            }
        }
    }
}
