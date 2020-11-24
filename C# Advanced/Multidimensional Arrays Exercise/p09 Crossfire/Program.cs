using System;
using System.Collections.Generic;
using System.Linq;

namespace p09_Crossfire
{
    class Program
    {
        static void Main(string[] args)
        {
            var dimensions = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var matrix = new int[dimensions[0]][];
            var counter = 1;
            for (int r = 0; r < matrix.Length; r++)
            {
                matrix[r] = new int[dimensions[1]];
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    matrix[r][c] = counter++;
                }
            }

            var input = Console.ReadLine();
            while (input != "Nuke it from orbit")
            {
                var tokens = input
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                var targetRow = tokens[0];
                var targetCol = tokens[1];
                var radius = tokens[2];

                for (int r = Math.Max(0, targetRow - radius); r <= Math.Min(matrix.Length - 1, targetRow + radius); r++)
                {
                    if (targetCol <= matrix[r].Length - 1 && targetCol >= 0)
                    {
                        matrix[r][targetCol] = -1;
                    }

                    if (r == targetRow)
                    {
                        for (int c = Math.Max(0, targetCol - radius); c <= Math.Min(matrix[r].Length - 1, targetCol + radius); c++)
                        {
                            matrix[r][c] = -1;
                        }
                    }
                }

                var remainingArrays = new List<int[]>();
                for (int r = 0; r < matrix.Length; r++)
                {
                    var currentArr = matrix[r].Where(e => e != -1).ToArray();
                    if (currentArr.Length > 0)
                    {
                        remainingArrays.Add(currentArr);
                    }
                }

                var newMatrix = new int[remainingArrays.Count][];
                for (int r = 0; r < newMatrix.Length; r++)
                {
                    var current = remainingArrays[r];
                    newMatrix[r] = new int[current.Length];
                    newMatrix[r] = current;
                }

                matrix = newMatrix;

                input = Console.ReadLine();
            }

            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if (matrix[r][c] != -1)
                    {
                        Console.Write(matrix[r][c] + " ");
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
