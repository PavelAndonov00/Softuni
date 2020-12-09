using System;

namespace RecursiveFibonacci
{
    class Program
    {
        static int n;
        static void Main(string[] args)
        {
            n = int.Parse(Console.ReadLine());
            var result = FindFibonacci2(n);
            Console.WriteLine(result);
        }
        private static int FindFibonacci2(int n)
        {
            if(n <= 1)
            {
                return 1;
            }

            var sum = FindFibonacci2(n - 1) + FindFibonacci2(n - 2);
            return sum;
        }

            private static int FindFibonacci(int previous, int next, int steps)
        {
            if (steps == n)
            {
                return 1;
            };

            var sum = previous + FindFibonacci(next, previous + next, steps + 1);
            return sum;
        }
    }
}
