using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_Reverse_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var stack = new Stack<int>(numbers);
            while (stack.Count > 1) 
            {
                Console.Write(stack.Pop() + " ");
            }

            Console.Write(stack.Pop() + "\n");
        }
    }
}
