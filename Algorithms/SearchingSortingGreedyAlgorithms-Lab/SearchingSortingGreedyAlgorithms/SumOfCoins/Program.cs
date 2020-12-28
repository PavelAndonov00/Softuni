using System;
using System.Collections.Generic;
using System.Linq;

namespace SumOfCoins
{
    class Program
    {
        static Stack<int> coins;
        static int needed;
        static int currentSum;
        static Dictionary<int, int> counts;
        static void Main(string[] args)
        {
            counts = new Dictionary<int, int>();
            coins = new Stack<int>(new int[2] { 3, 7 });
            needed = 11;
            Find(coins.Pop());
        }

        private static void Find(int currentCoin)
        {
            var currentCoinsToTake = (needed - currentSum) / currentCoin;
            var coinsSum = currentCoin * currentCoinsToTake;
            currentSum += coinsSum;
            counts[currentCoin] = currentCoinsToTake;
            if (currentSum > needed)
            {
                currentSum -= coinsSum;
                counts.Remove(currentCoin);
                Find(coins.Pop());
            }
            else if(currentSum == needed)
            {
                PrintSolution();
            }
            else if(coins.Count == 0)
            {
                Console.WriteLine("No solution");
            }
            else
            {
                Find(coins.Pop());
            }
        }

        private static void PrintSolution()
        {
            Console.WriteLine("Number of coins to take: " + counts.Sum(x => x.Value));
            foreach (var kvp in counts)
            {
                Console.WriteLine($"{kvp.Value} coin(s) with value {kvp.Key}");
            }
        }
    }
}
