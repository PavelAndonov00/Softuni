using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace p10_Simple_Text_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfOperations = long.Parse(Console.ReadLine());

            var lastStatesOfTheText = new Stack<string>();
            lastStatesOfTheText.Push("");
            for (int i = 0; i < numberOfOperations; i++)
            {
                var input = Console.ReadLine().Split();

                switch (input[0])
                {
                    case "1":
                        var newState = lastStatesOfTheText.Peek() + input[1];
                        lastStatesOfTheText.Push(newState);
                        break;
                    case "2":
                        var last = lastStatesOfTheText.Peek();

                        var count = int.Parse(input[1]);
                        var startIndex = last.Length - count;

                        lastStatesOfTheText.Push(last.Substring(0, startIndex));
                        break;
                    case "3":
                        var index = int.Parse(input[1]) - 1;
                        Console.WriteLine(lastStatesOfTheText.Peek()[index]);
                        break;
                    case "4":
                        lastStatesOfTheText.Pop();
                        break;
                }
            }
        }
    }
}
