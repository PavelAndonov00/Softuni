using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_Periodic_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var periodicTable = new SortedSet<string>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .ToArray();

                foreach (var compound in input)
                {
                    periodicTable.Add(compound);
                }
            }

            Console.WriteLine(String.Join(" ", periodicTable));
        }
    }
}
