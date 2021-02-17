using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestStringChain
{
    class Program
    {
        static string[] sequence;
        static int[] len;
        static int[] prev;
        static int lastIndex;
        static void Main(string[] args)
        {
            sequence = Console.ReadLine()
                .Split();
            len = new int[sequence.Length];
            prev = new int[sequence.Length];

            GetLongestStringChain();

            var result = new Stack<string>();
            result.Push(sequence[lastIndex]);
            while (prev[lastIndex] != -1)
            {
                result.Push(sequence[prev[lastIndex]]);
                lastIndex = prev[lastIndex];
            }
            Console.WriteLine(string.Join(" ", result));
        }

        private static void GetLongestStringChain()
        {
            var maxLen = -1;
            for (int i = 0; i < sequence.Length; i++)
            {
                var bestLen = 1;
                var prevIdx = -1;
                for (int k = i - 1; k >= 0; k--)
                {
                    if(sequence[i].Length > sequence[k].Length &&
                        len[k] + 1 >= bestLen)
                    {
                        bestLen = len[k] + 1;
                        prevIdx = k;
                    }
                }

                len[i] = bestLen;
                prev[i] = prevIdx;

                if(bestLen > maxLen)
                {
                    maxLen = bestLen;
                    lastIndex = i;
                }
            }
        }
    }
}
