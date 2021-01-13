using System;
using System.Collections.Generic;
using System.Linq;

namespace Climbing
{
    class Program
    {
        static int[,] matrix;
        static int[,] sum;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());
            matrix = new int[n, m];
            sum = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                var row = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (int k = 0; k < row.Length; k++)
                {
                    matrix[i, k] = row[k];
                }
            }

            sum[0, 0] = matrix[0, 0];
            for (int i = 1; i < m; i++)
            {
                sum[0, i] = matrix[0, i] + sum[0, i - 1];
            }

            for (int i = 1; i < n; i++)
            {
                sum[i, 0] = matrix[i, 0] + sum[i - 1, 0];
            }

            for (int i = 1; i < n; i++)
            {
                for (int k = 1; k < m; k++)
                {
                    if (sum[i - 1, k] >= sum[i, k - 1])
                    {
                        sum[i, k] = matrix[i, k] + sum[i - 1, k];
                    }
                    else
                    {
                        sum[i, k] = matrix[i, k] + sum[i, k - 1];
                    }
                }
            }

            var currentRow = n - 1;
            var currentCol = m - 1;
            Console.WriteLine(sum[currentRow, currentCol]);
            var stack = new List<int>();
            stack.Add(matrix[currentRow, currentCol]);
            while (currentRow > 0 && currentCol > 0)
            {
                if(sum[currentRow - 1, currentCol] >= sum[currentRow, currentCol - 1])
                {
                    currentRow--;
                }
                else
                {
                    currentCol--;
                }

                stack.Add(matrix[currentRow, currentCol]);
            }

            while (currentCol > 0)
            {
                currentCol--;
                stack.Add(matrix[currentRow, currentCol]);
            }

            while (currentRow > 0)
            {
                currentRow--;
                stack.Add(matrix[currentRow, currentCol]);
            }

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}
