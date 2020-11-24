using System;
using System.Linq;

namespace p01
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsAndCols = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rowsCount = rowsAndCols[0];
            var colsCount = rowsAndCols[1];
            var sum = 0;
            for (int row = 0; row < rowsCount; row++)
            {
                var colNumbers = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
                for (int col = 0; col < colsCount; col++)
                {
                    sum += colNumbers[col];
                }
            }

            Console.WriteLine(rowsCount);
            Console.WriteLine(colsCount);
            Console.WriteLine(sum);               
        }
    }
}
