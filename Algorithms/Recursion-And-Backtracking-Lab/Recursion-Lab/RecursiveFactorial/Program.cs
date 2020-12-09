using System;

namespace RecursiveFactorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var sum = CalcFactorial(n);
            Console.WriteLine(sum);
        }

        private static int CalcFactorial(int n)
        {
            if(n == 0)
            {
                return 1;
            }
            var sum = n * CalcFactorial(n - 1);
            return sum;
        }
    }
}
