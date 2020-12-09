using System;

namespace _8QueensProblem
{
    class Program
    {
        static char[,] matrix;
        static void Main(string[] args)
        {
            matrix = new char[8, 8];
            PopulateMatrix();
            FindSolution(0);
        }

        private static void PopulateMatrix()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    matrix[i, k] = '-';
                }
            }
        }

        private static void FindSolution(int row)
        {
            if (row == 8)
            {
                PrintSolution();
                // Backtracking
                return;
            }

            for (int i = 0; i < 8; i++)
            {
                if (CanPlaced(row, i))
                {
                    Mark(row, i);
                    FindSolution(row + 1);
                    UnMark(row, i);
                }
            }
        }

        private static void PrintSolution()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    if (k == 7)
                    {
                        Console.Write(matrix[i, k]);
                        break;
                    }
                    Console.Write(matrix[i, k] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool CanPlaced(int row, int col)
        {
            // Down
            for (int i = row + 1; i < 8; i++)
            {
                if (CheckIfFree(i, col))
                {
                    return false;
                }
            }

            // Right
            for (int i = col + 1; i < 8; i++)
            {
                if (CheckIfFree(row, i))
                {
                    return false;
                }
            }

            // Up
            for (int i = row - 1; i >= 0; i--)
            {
                if (CheckIfFree(i, col))
                {
                    return false;
                }
            }

            // Left
            for (int i = col - 1; i >= 0; i--)
            {
                if (CheckIfFree(row, i))
                {
                    return false;
                }
            }

            // Down right diagonal
            var k = 1;
            for (int i = row + 1; i < 8; i++, k++)
            {
                var currentCol = col + k;
                if (currentCol > 7) break;

                if (CheckIfFree(i, currentCol))
                {
                    return false;
                }
            }

            // Down left diagonal
            k = 1;
            for (int i = row + 1; i < 8; i++, k++)
            {
                var currentCol = col - k;
                if (currentCol < 0) break;

                if (CheckIfFree(i, currentCol))
                {
                    return false;
                }
            }

            // Up right diagonal
            k = 1;
            for (int i = row - 1; i >= 0; i--, k++)
            {
                var currentCol = col + k;
                if (currentCol > 7) break;

                if (CheckIfFree(i, currentCol))
                {
                    return false;
                }
            }

            // Up left diagonal
            k = 1;
            for (int i = row - 1; i >= 0; i--, k++)
            {
                var currentCol = col - k;
                if (currentCol < 0) break;

                if (CheckIfFree(i, currentCol))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool CheckIfFree(int row, int col)
        {
            return matrix[row, col] == '*';
        }

        private static void UnMark(int row, int col)
        {
            matrix[row, col] = '-';
        }

        private static void Mark(int row, int col)
        {
            matrix[row, col] = '*';
        }

        private static bool IsOutOfBound(int row, int col)
        {
            throw new NotImplementedException();
        }
    }
}
