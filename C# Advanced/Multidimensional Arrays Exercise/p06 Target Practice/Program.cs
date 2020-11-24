using System;
using System.Linq;

namespace p06_Target_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsAndCols = Console.ReadLine()
                 .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();

            var rowsCount = rowsAndCols[0];
            var colsCount = rowsAndCols[1];

            var fillString = Console.ReadLine();

            var rowColAndRadius = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var targetRow = rowColAndRadius[0];
            var targetCol = rowColAndRadius[1];
            var targetRadius = rowColAndRadius[2];

            var matrix = new string[rowsCount, colsCount];
            for (int matrixRow = matrix.GetLength(0) - 1; matrixRow >= 0; matrixRow--)
            {
                if (matrixRow % 2 == 0)
                {
                    for (int matrixCol = matrix.GetLength(1) - 1; matrixCol >= 0; matrixCol--)
                    {
                        var current = fillString.Substring(0, 1);
                        matrix[matrixRow, matrixCol] = current;
                        fillString = fillString.Substring(1) + current;
                    }
                }
                else
                {
                    for (int matrixCol = 0; matrixCol < matrix.GetLength(1); matrixCol++)
                    {
                        var current = fillString.Substring(0, 1);
                        matrix[matrixRow, matrixCol] = current;
                        fillString = fillString.Substring(1) + current;
                    }
                }
            }

            var startRow = Math.Max(0, targetRow - targetRadius);
            var endRow = Math.Min(matrix.GetLength(0) - 1, targetRow + targetRadius);

            var startCol = Math.Max(0, targetCol - targetRadius);
            var endCol = Math.Min(matrix.GetLength(1) - 1, targetCol + targetRadius);
            for (int r = startRow; r <= endRow; r++)
            {
                for (int c = startCol; c <= endCol; c++)
                {
                    if (Math.Sqrt(Math.Pow(targetRow - r, 2) + Math.Pow(targetCol - c, 2)) <= targetRadius)
                    {
                        matrix[r, c] = " ";
                    }
                }
            }

            CleanUp(matrix);

            PrintOnTheConsole(matrix);
        }

        private static void CleanUp(string[,] matrix)
        {
            for (int k = 0; k < matrix.GetLength(0); k++)
            {
                for (int r = 0; r < matrix.GetLength(0)-1; r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c] != " " && matrix[r + 1, c] == " ")
                        {
                            matrix[r + 1, c] = matrix[r, c];
                            matrix[r, c] = " ";
                        }
                    }
                }
            }
        }

        private static void PrintOnTheConsole(string[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }
                Console.WriteLine();
            }
        }       
    }
}
