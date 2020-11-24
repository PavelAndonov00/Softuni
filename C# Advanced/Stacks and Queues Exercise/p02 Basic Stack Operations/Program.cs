using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Basic_Stack_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var numbersToPush = operations[0]; 
            var numbersToPop = operations[1]; 
            var numberToLookFor = operations[2]; 

            var numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();
            numbers = numbers.Take(numbersToPush).ToArray();

            var stack = new Stack<int>(numbers);

            for (int i = 0; i < numbersToPop; i++)
            {
                stack.Pop();
            }

            if(stack.Count == 0)
            {
                Console.WriteLine(0);
            }
            else if(stack.Contains(numberToLookFor))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine(stack.Min());
            }
        }
    }
}
