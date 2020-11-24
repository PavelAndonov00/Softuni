using System;

namespace p08_Recursive_Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var first = 1;
            var second = 1;
            for (int i = 0; i < n; i++)
            {
                var next = first + second;

                first = second;
                second = next;
            }

            Console.WriteLine(second - first);
        }
    }
}
