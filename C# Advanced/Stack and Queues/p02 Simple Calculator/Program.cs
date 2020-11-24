using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Simple_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(" ");
            var stack = new Stack<string>(input.Reverse());

            while (stack.Count > 1)
            {
                var leftOperand = int.Parse(stack.Pop());
                var operation = stack.Pop();
                var rightOperand = int.Parse(stack.Pop());

                var sum = 0;
                switch (operation)
                {
                    case "+":
                        sum = leftOperand + rightOperand;
                        break;
                    case "-":
                        sum = leftOperand - rightOperand;
                        break;
                }

                stack.Push(sum.ToString());
            }

            Console.WriteLine(stack.Peek());
        }
    }
}
