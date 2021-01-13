using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperSet
{
    class Program
    {
        static int[] numbers;
        static List<int> combinations;
        static bool[] used;
        static void Main(string[] args)
        {
            numbers = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            for (int i = 1; i <= numbers.Length; i++)
            {
                combinations = new List<int>();
                used = new bool[numbers.Length];
                GenerateSuperSet(0, i);
            }
           
        }

        private static void GenerateSuperSet(int elStartIdx, int count)
        {
            if(combinations.Count == count)
            {
                Console.WriteLine(string.Join(" ", combinations));
                return;
            }
            
            for (int i = elStartIdx; i < numbers.Length; i++)
            {
                combinations.Add(numbers[i]);
                GenerateSuperSet(i + 1, count);
                combinations.RemoveAt(combinations.Count - 1);
            }
        }
    }
}
