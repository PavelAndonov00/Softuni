namespace TwoMinitesToMidnight
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    class Program
    {
        static Dictionary<KeyValuePair<int, int>, decimal> cache;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());
            cache = new Dictionary<KeyValuePair<int, int>, decimal>();
            Console.WriteLine(GetBinom(n, k));
        }

        private static decimal GetBinom(int row, int col)
        {
            if (row == 0 || row == 1 || col == 0 || col == row)
            {
                return 1;
            }

            var kvp = new KeyValuePair<int, int>(row, col);
            if (!cache.ContainsKey(kvp))
            {
                cache[kvp] = GetBinom(row - 1, col - 1) + GetBinom(row - 1, col);
            }

            return cache[kvp];
        }
    }

}
