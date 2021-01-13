using System;
using System.Collections.Generic;

namespace Fibonacci
{
    class Program
    {
        static Dictionary<decimal, decimal> calculated;
        static void Main(string[] args)
        {
            calculated = new Dictionary<decimal, decimal>();
            var n = decimal.Parse(Console.ReadLine());
            var result = FindFibRecursiveOptimized(n-1);
            Console.WriteLine(result);
        }

        private static decimal FindFibRecursiveOptimized(decimal n)
        {
            if (n == 1)
            {
                return 1;
            }

            if (n == 2)
            {
                return 2;
            }

            if (!calculated.ContainsKey(n))
            {
                calculated[n] = FindFibRecursiveOptimized(n - 1) + FindFibRecursiveOptimized(n - 2);
            }

            return calculated[n];
        }

        private static decimal FindFibRecursive(decimal n)
        {
            if(n == 1)
            {
                return 1;
            }

            if(n == 2)
            {
                return 2;
            }

            return FindFibRecursive(n - 1) + FindFibRecursive(n - 2);
        }

        private static decimal FindFibIterative(decimal n)
        {
            var first = 0;
            var second = 1;
            for (int i = 0; i < n - 1; i++)
            {
                var lastSecond = second;
                second = first + second;
                first = lastSecond;
            }

            return second;
        }


    }
}
