using System;
using System.Collections.Generic;

namespace p06_Stack_Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = int.Parse(Console.ReadLine());

            var stack = new Stack<int>();
            stack.Push(0);
            stack.Push(1);
            for (int i = 0; i < number; i++)
            {
                var second = stack.Pop();
                var first = stack.Pop();

                stack.Push()
            }
        }
    }
}
