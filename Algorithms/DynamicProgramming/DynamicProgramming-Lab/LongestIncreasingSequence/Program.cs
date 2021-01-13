using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LongestIncreasingSequence
{
    class Program
    {
        static int[] sequence;
        static List<int>[] subSequencesCount;
        static void Main(string[] args)
        {
            sequence = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            subSequencesCount = new List<int>[sequence.Length];
            FindLongestSequence();
            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine(string.Join(" ", subSequencesCount
                .OrderByDescending(sub => sub.Count)
                .FirstOrDefault()));
        }

        private static void FindLongestSequence()
        {
            for (int index = sequence.Length - 1; index >= 0; index--)
            {
                subSequencesCount[index] = new List<int> { sequence[index] };

                var currentLongest = 0;
                var longestIndex = -1;
                for (int k = index + 1; k < subSequencesCount.Length; k++)
                {
                    if (subSequencesCount[k].Count > currentLongest && sequence[k] > sequence[index])
                    {
                        currentLongest = subSequencesCount[k].Count;
                        longestIndex = k;
                    }
                }

                if (longestIndex > -1)
                {
                    subSequencesCount[index].AddRange(subSequencesCount[longestIndex]);
                }
            }
        }
    }
}
