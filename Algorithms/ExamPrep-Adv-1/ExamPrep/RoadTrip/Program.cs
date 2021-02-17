using System;
using System.Linq;

namespace RoadTrip
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = Console.ReadLine()
                .Split(new char[] { ',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var capacities = Console.ReadLine()
               .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse)
               .ToArray();
            var maxCapacity = int.Parse(Console.ReadLine());

            var dp = new int[values.Length + 1, maxCapacity + 1];
            for (int row = 1; row < dp.GetLength(0); row++)
            {
                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    if(capacity >= capacities[row - 1])
                    {
                        dp[row, capacity] = Math.Max(dp[row - 1, capacity],
                            dp[row - 1, capacity - capacities[row - 1]] + values[row - 1]);
                    }
                    else
                    {
                        dp[row, capacity] = dp[row - 1, capacity];
                    }
                }
            }

            Console.WriteLine($"Maximum value: {dp[dp.GetLength(0) - 1, dp.GetLength(1) - 1]}");
        }
    }
}
