using System;
using System.Collections.Generic;
using System.Linq;

namespace RodCutting
{
    class Program
    {
        static int[] bestPrices;
        static int[] bestCombo;
        static int counter;
        static void Main(string[] args)
        {
            var lengths = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var needed = int.Parse(Console.ReadLine());

            bestPrices = new int[lengths.Length];
            bestCombo = new int[lengths.Length];
            Console.WriteLine(RodCut(needed, lengths));

            var result = new List<int>();
            while (bestCombo[needed] != 0)
            {
                result.Add(bestCombo[needed]);
                needed -= bestCombo[needed];
            }
            result.Add(needed);
            Console.WriteLine(string.Join(" ", result));
            Console.WriteLine("Counter: " + counter);
        }

        private static int RodCut(int length, int[] lengths)
        {
            counter++;
            if (length == 0)
            {
                return 0;
            }

            if(bestPrices[length] > 0)
            {
                return bestPrices[length];
            }

            var bestPrice = lengths[length];
            bestPrices[length] = bestPrice;
            for (int i = 1; i < length; i++)
            {
                var currentSum = lengths[i] + RodCut(length - i, lengths);
                
                if (currentSum > bestPrice)
                {
                    bestCombo[length] = i;
                    bestPrice = currentSum;
                    bestPrices[length] = bestPrice;
                }
            }

            return bestPrices[length];
        }
    }
}
