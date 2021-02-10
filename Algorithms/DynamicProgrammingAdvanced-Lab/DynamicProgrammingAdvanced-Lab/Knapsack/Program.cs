using System;
using System.Collections.Generic;

namespace Knapsack
{
    class Program
    {
        class Item
        {
            public string Name { get; set; }

            public int Weight { get; set; }

            public int Value { get; set; }
        }
        static int[,] sack;
        static List<Item> items;
        static void Main(string[] args)
        {
            var capacity = int.Parse(Console.ReadLine());
            items = ReadItems();
            sack = new int[items.Count + 1, capacity + 1];
            FillSack();
            PrintInfo();
        }

        private static void PrintInfo()
        {
            /*
                Backtracking start at the end index 
                If current cell is different from the same cell on the previous row,
                the item on the current row is in sack
             */
            var result = new SortedSet<string>();
            var row = sack.GetLength(0) - 1;
            var capacity = sack.GetLength(1) - 1;
            var totalWeight = 0;
            var totalValue = 0;

            while (row > 0 && capacity > 0)
            {
                if(sack[row, capacity] != sack[row - 1, capacity])
                {
                    var currentItem = items[row - 1];
                    result.Add(currentItem.Name);
                    totalWeight += currentItem.Weight;
                    totalValue += currentItem.Value;

                    capacity -= currentItem.Weight;
                }

                row--;
            }

            Console.WriteLine($"Total Weight: {totalWeight}");
            Console.WriteLine($"Total Value: {totalValue}");
            Console.WriteLine(string.Join("\r\n", result));
        }

        private static void FillSack()
        {
            /*
                matrix[row - 1, capacity] excluding item in sack,
                matrix[row - 1, capacity - currentItem.Weight] including item in sack,
                if going to take -> Math.Max(excluding, including)
                if not -> excluding
             */
            for (int row = 1; row < sack.GetLength(0); row++)
            {
                var currentItem = items[row - 1];
                for (int capacity = 0; capacity < sack.GetLength(1); capacity++)
                {
                    var excluding = sack[row - 1, capacity];
                    if (capacity > currentItem.Weight)
                    {
                        var including = sack[row - 1, capacity - currentItem.Weight] + currentItem.Value;
                        sack[row, capacity] = Math.Max(excluding, including);
                    }
                    else
                    {
                        sack[row, capacity] = excluding;
                    }
                }
            }
        }

        private static List<Item> ReadItems()
        {
            var result = new List<Item>();
            var input = Console.ReadLine()
                .Split();

            while (input[0] != "end")
            {
                result.Add(new Item
                {
                    Name = input[0],
                    Weight = int.Parse(input[1]),
                    Value = int.Parse(input[2])
                });

                input = Console.ReadLine()
                .Split();
            }

            return result;
        }
    }
}
