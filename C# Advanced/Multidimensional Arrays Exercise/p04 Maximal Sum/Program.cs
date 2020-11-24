using System;
using System.Linq;

namespace p04_Maximal_Sum
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsAndColsCount = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rowsCount = rowsAndColsCount[0];
            var colsCount = rowsAndColsCount[1];
            var matrix = new int[rowsCount, colsCount];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var colsNumbers = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colsNumbers[col];
                }
            }

            var sumMax = int.MinValue;
            var resultRow = 0;
            var resultCol = 0;
            for (int row = 0; row < matrix.GetLength(0)-2; row++)
            {
                for (int col = 0; col < matrix.GetLength(1)-2; col++)
                {
                    var sum = matrix[row, col] + matrix[row, col+1] + matrix[row, col+2] +
                        matrix[row+1, col] + matrix[row+1, col+1] + matrix[row+1, col+2] +
                        matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];


                    if (sum > sumMax)
                    {
                        sumMax = sum;

                        resultRow = row;
                        resultCol = col;
                    }
                }
            }

            Console.WriteLine("Sum = " + sumMax);
            Console.WriteLine(matrix[resultRow, resultCol] + " " + matrix[resultRow, resultCol+1] + " " + matrix[resultRow, resultCol+2]);
            Console.WriteLine(matrix[resultRow+1, resultCol] + " " + matrix[resultRow+1, resultCol+1] + " " + matrix[resultRow+1, resultCol+2]);
            Console.WriteLine(matrix[resultRow+2, resultCol] + " " + matrix[resultRow+2, resultCol+1] + " " + matrix[resultRow+2, resultCol+2]);
        }
    }
}
