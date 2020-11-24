using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Square_With_Maximum_Sum
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
            var matrix = new int[rowsCount, colsCount];
            for (int row = 0; row < rowsCount; row++)
            {
                var colNumbers = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
                for (int col = 0; col < colsCount; col++)
                {
                    matrix[row, col] = colNumbers[col];
                }
            }
            
            var sumMax = int.MinValue;
            var result = new List<int>();
            for (int row = 0; row < matrix.GetLength(0)-1; row++)
            {
               
                for (int col = 0; col < matrix.GetLength(1)-1; col++)
                {
                    var sum = 0;
                    sum += matrix[row, col];
                    sum += matrix[row, col+1];
                    sum += matrix[row+1, col];
                    sum += matrix[row+1, col+1];

                    if(sum > sumMax)
                    {
                        sumMax = sum;                        
                        result.Clear();
                        result.Add(matrix[row, col]);
                        result.Add(matrix[row, col+1]);
                        result.Add(matrix[row+1, col]);
                        result.Add(matrix[row+1, col+1]);
                    }
                }
            }

            Console.WriteLine(String.Join(" ", result.Take(2)));
            Console.WriteLine(String.Join(" ", result.Skip(2)));
            Console.WriteLine(sumMax);
        }
    }
}
