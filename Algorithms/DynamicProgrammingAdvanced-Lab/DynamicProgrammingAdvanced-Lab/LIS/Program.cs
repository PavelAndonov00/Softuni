using System;
using System.Collections.Generic;
using System.Linq;

namespace LIS
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var len = new int[sequence.Length];
            var prev = new int[sequence.Length];
            FindLis(sequence, len, prev);
        }

        private static void FindLis(int[] sequence, int[] len, int[] prev)
        {
            var bestLength = 0;
            var lastIndex = 0;
            for (int curIdx = 0; curIdx < sequence.Length; curIdx++)
            {
                var currentNumber = sequence[curIdx];
                var currentLength = 1;
                var prevIndex = -1;
                for (int prevIdx = curIdx - 1; prevIdx >= 0; prevIdx--)
                {
                    var previousNumber = sequence[prevIdx];
                    // len[prevIdx] + 1 ---> Length of the previous plus current
                    // len[prevIdx] + 1 >= currentLength ---> finding left-most
                    if (previousNumber < currentNumber && len[prevIdx] + 1 >= currentLength)
                    {
                        currentLength = len[prevIdx] + 1;   
                        prevIndex = prevIdx;
                    }
                }

                len[curIdx] = currentLength;
                prev[curIdx] = prevIndex;

                // Storing best length to obtain the last index of the obtained max length ---> lastIndex = curIdx
                if (bestLength < currentLength)
                {
                    bestLength = currentLength;
                    lastIndex = curIdx;
                }
            }

            Print(lastIndex, prev, sequence);
        }

        private static void Print(int lastIndex, int[] prev, int[] sequence)
        {
            //Reconstructing path
            var result = new Stack<int>();
            result.Push(sequence[lastIndex]);
            while (prev[lastIndex] != -1)
            {
                lastIndex = prev[lastIndex];
                result.Push(sequence[lastIndex]);
            }

            Console.WriteLine(string.Join(" ", result));
        }
    }
}
