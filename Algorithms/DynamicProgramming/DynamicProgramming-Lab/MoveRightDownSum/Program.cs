using System;
using System.Collections.Generic;
using System.Linq;

namespace MoveRightDownSum
{
    class Program
    {
        static int[,] matrix;
        static int[,] sums;
        static void Main(string[] args)
        {
            var rowCount = int.Parse(Console.ReadLine());
            var colCount = int.Parse(Console.ReadLine());

            matrix = new int[rowCount, colCount];
            PopulateMatrix(rowCount, colCount);
            sums = new int[rowCount, colCount];
            CalculateMatrixCells(rowCount, colCount);
            PrintResult2(rowCount, colCount);
        }

        private static void PrintResult(int rowCount, int colCount)
        {
            var currentRow = 0;
            var currentCol = 0;
            Console.Write($"[{currentRow}, {currentCol}] ");
            while (true)
            {
                if (currentRow + 1 < rowCount && currentCol + 1 < colCount)
                {
                    if (sums[currentRow, currentCol + 1] < sums[currentRow + 1, currentCol])
                    {
                        Console.Write($"[{currentRow}, {currentCol + 1}] ");
                        currentCol++;
                    }
                    else
                    {
                        Console.Write($"[{currentRow + 1}, {currentCol}] ");
                        currentRow++;
                    }
                }
                else if (currentRow + 1 < rowCount)
                {
                    for (int r = currentRow + 1; r < rowCount; r++)
                    {
                        Console.Write($"[{r}, {currentCol}] ");
                    }
                    break;
                }
                else if (currentCol + 1 < colCount)
                {
                    for (int c = currentCol + 1; c < colCount; c++)
                    {
                        Console.Write($"[{currentRow}, {c}] ");
                    }
                    break;
                }
            }
        }
        private static void PrintResult2(int rowCount, int colCount)
        {
            var curRow = rowCount - 1;
            var curCol = colCount - 1;
            var result = new Stack<string>();
            result.Push($"[{curRow}, {curCol}]");
            while (curRow > 0 && curCol > 0)
            {
                var upper = sums[curRow - 1, curCol];
                var left = sums[curRow, curCol - 1];
                if (upper > left)
                {
                    curRow -= 1;
                }
                else
                {
                    curCol -= 1;
                }

                result.Push($"[{curRow}, {curCol}]");
            }

            while (curRow >  0)
            {
                curRow -= 1;
                result.Push($"[{curRow}, {curCol}]");
            }

            while (curCol >  0)
            {
                curCol -= 1;
                result.Push($"[{curRow}, {curCol}]");
            }

            Console.WriteLine(string.Join(" ", result));
        }

        private static void PopulateMatrix(int rowCount, int colCount)
        {
            for (int r = 0; r < rowCount; r++)
            {
                var row = Console.ReadLine()
                    .Split();
                for (int c = 0; c < colCount; c++)
                {
                    matrix[r, c] = int.Parse(row[c]);
                }
            }
        }

        private static void CalculateMatrixCells(int rowCount, int colCount)
        {
            sums[0, 0] = matrix[0, 0];
            for (int c = 1; c < colCount; c++)
            {
                sums[0, c] = sums[0, c - 1] + matrix[0, c];
            }

            for (int r = 1; r < rowCount; r++)
            {
                sums[r, 0] = sums[r - 1, 0] + matrix[r, 0];
            }

            for (int row = 1; row < rowCount; row++)
            {
                for (int col = 1; col < colCount; col++)
                {
                    sums[row, col] = Math.Max(sums[row - 1, col], sums[row, col - 1]) + matrix[row, col];
                }
            }
        }
    }
}
