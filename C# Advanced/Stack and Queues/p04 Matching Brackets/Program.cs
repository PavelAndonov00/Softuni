using System;
using System.Collections.Generic;

namespace p04_Matching_Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var stack = new Stack<int>();

            for (int i = 0; i < input.Length; i++)
            {
                if(input[i] == '(')
                {
                    stack.Push(i);
                }
                else if (input[i] == ')')
                {
                    var openBracketIndex = stack.Pop();
                    var closingBracketIndex = i;

                    var expressionLength = closingBracketIndex - openBracketIndex + 1;
                    Console.WriteLine(input.Substring(openBracketIndex, expressionLength));
                }
            }
        }
    }
}
