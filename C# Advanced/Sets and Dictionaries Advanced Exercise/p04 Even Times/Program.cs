using System;
using System.Collections.Generic;

namespace p04_Even_Times
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var numbers = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                var number = int.Parse(Console.ReadLine());
                if (!numbers.ContainsKey(number))
                {
                    numbers[number] = 0;
                }

                numbers[number]++;
            }

            foreach (var number in numbers.Keys)
            {
                var current = numbers[number];
                if (current % 2 == 0)
                {
                    Console.WriteLine(number);
                    break;
                }
            }
        }
    }
}
