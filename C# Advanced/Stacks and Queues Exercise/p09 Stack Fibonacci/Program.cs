using System;
using System.Collections.Generic;

namespace p09_Stack_Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = long.Parse(Console.ReadLine());

            var stack = new Stack<long>();
            stack.Push(0);
            stack.Push(1);
            for (int i = 0; i < number; i++)
            {
                var second = stack.Pop();
                var first = stack.Pop();

                stack.Push(second);
                stack.Push(first + second);
            }

            stack.Pop();
            Console.WriteLine(stack.Pop());
        }
    }
}
