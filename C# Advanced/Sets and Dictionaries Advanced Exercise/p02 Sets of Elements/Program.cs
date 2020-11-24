using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Sets_of_Elements
{
    class Program
    {
        static void Main(string[] args)
        {
            var counts = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var firstSetCount = counts[0];
            var firstSet = new HashSet<int>();
            for (int i = 0; i < firstSetCount; i++)
            {
                firstSet.Add(int.Parse(Console.ReadLine()));
            }

            var secondSetCount = counts[1];
            var secondSet = new HashSet<int>();
            for (int i = 0; i < secondSetCount; i++)
            {
                secondSet.Add(int.Parse(Console.ReadLine()));
            }

            foreach (var number in firstSet)
            {
                if(secondSet.Contains(number))
                {
                    Console.Write(number + " ");
                }
            }
        }
    }
}
