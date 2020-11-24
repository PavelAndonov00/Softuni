using System;
using System.Linq;

namespace p02_Sum_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            Console.WriteLine(arr.Length);
            Console.WriteLine(arr.Sum());
        }
    }
}
