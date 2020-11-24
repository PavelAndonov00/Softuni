using System;
using System.Collections.Generic;
using System.Linq;

namespace p01_Count_Same_Values_in_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split()
                .Select(double.Parse)
                .ToArray();

            var result = new Dictionary<double, int>();
            foreach (var number in input)
            {
                if(!result.ContainsKey(number))
                {
                    result[number] = 0;
                }

                result[number]++;
            }

            foreach (var key in result.Keys)
            {
                Console.WriteLine($"{key} - {result[key]} times");
            }
        }
    }
}
