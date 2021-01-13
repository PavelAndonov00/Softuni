using System;
using System.Collections.Generic;
using System.Linq;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var secondSequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var table = new int[firstSequence.Length + 1, secondSequence.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (firstSequence[r - 1] == secondSequence[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            var row = firstSequence.Length;
            var col = secondSequence.Length;
            var stack = new Stack<int>();
            while (row > 0 && col > 0)
            {
                if (firstSequence[row - 1] == secondSequence[col - 1])
                {
                    row--;
                    col--;
                    stack.Push(firstSequence[row]);
                }
                else if (table[row - 1, col] > table[row, col - 1])
                {
                    row--;
                }
                else
                {
                    col--;
                }
            }
            Console.WriteLine(string.Join(" ", stack));
            Console.WriteLine(stack.Count);
        }
    }
}
