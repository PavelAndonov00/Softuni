using System;
using System.Collections.Generic;

namespace p03_Decimal_to_Binary_Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());
            var stack = new Stack<int>();

            if(input == 0)
            {
                Console.WriteLine(0);
                return;
            }

            while (input > 1)
            {
                stack.Push(input % 2);

                input /= 2;
            }

            stack.Push(input);
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
            }
            Console.WriteLine();
        }
    }
}
