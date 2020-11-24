using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recursion_Lab
{
    public class Program
    {
        static long[,] matrix = new long[133, 133];
        public static void Main(string[] args)
        {
            FillMatrix(0, 0, 1, 's');
        }

        private static void FillMatrix(long row, long col, long number, char dir)
        {
            if (IsOutsideOfBounds(row, col))
            {
                return;
            }

            if (IsPassable(row, col, dir))
            {
                matrix[row, col] = number;

                FillMatrix(row, col + 1, number + 1, 'R');
                FillMatrix(row + 1, col, number + 1, 'D');
                FillMatrix(row, col - 1, number + 1, 'L');
                FillMatrix(row - 1, col, number + 1, 'U');
            }
            else
            {
                if (number == matrix.GetLength(0) * matrix.GetLength(1) + 1)
                {
                    PrintSolution();
                    Environment.Exit(0);
                }
            }
        }

        private static bool IsPassable(long row, long col, char dir)
        {
            if (!IsOutsideOfBounds(row - 1, col - 1))
            {
                if (matrix[row - 1, col - 1] == 0 && dir == 'R')
                {
                    return false;
                }
            }

            return matrix[row, col] == 0;
        }

        private static void PrintSolution()
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    string result;
                    if (matrix[row, col] < 10)
                    {
                        result = "0" + matrix[row, col].ToString() + " ";
                    }
                    else
                    {
                        result = matrix[row, col].ToString() + " ";
                    }

                    Console.Write(result);
                }
                Console.WriteLine();
            }
        }

        private static bool IsOutsideOfBounds(long row, long col)
        {
            return row >= matrix.GetLength(0) || row < 0 || col < 0 || col >= matrix.GetLength(1);
        }

        private static int RecursiveArraySum(int[] array, int index)
        {
            if (index > array.Length - 1)
            {
                return 0;
            }

            var sum = array[index] + RecursiveArraySum(array, index + 1);
            return sum;
        }

        private static long RecursiveFactorial(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            return n * RecursiveFactorial(n - 1);
        }

        private static void RecursiveDrawing(int n)
        {
            if (n <= 0)
            {
                return;
            }

            Console.WriteLine(new string('*', n));
            RecursiveDrawing(n - 1);
            Console.WriteLine(new string('#', n));
        }

        private static void GeneratingVectors(int[] vector, int index)
        {
            if (index > vector.Length - 1)
            {
                Console.WriteLine(string.Join("", vector));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;
                    GeneratingVectors(vector, index + 1);
                }
            }
        }

        private static void GenCombs(int[] set, int[] vector, int index, int border)
        {
            if (index == vector.Length)
            {
                Console.WriteLine(string.Join(" ", vector));
            }
            else
            {
                for (int i = border; i < set.Length; i++)
                {
                    vector[index] = set[i];
                    GenCombs(set, vector, index + 1, i + 1);
                }
            }
        }

        public class EightQueens
        {
            const int Size = 8;
            static bool[,] chessboard = new bool[Size, Size];
            static HashSet<int> attackedRows = new HashSet<int>();
            static HashSet<int> attackedCols = new HashSet<int>();

            public static void PutQueens(int row)
            {
                if (row == Size)
                {
                    PrintSolution();
                }
                else
                {
                    for (int col = 0; col < Size; col++)
                    {
                        if (CanPlaceQueen(row, col))
                        {
                            MarkAllAttackedPositions(row, col);
                            PutQueens(row + 1);
                            UnmarkAllAttackedPositions(row, col);
                        }
                    }
                }
            }

            private static void UnmarkAllAttackedPositions(int row, int col)
            {
                attackedRows.Remove(row);
                attackedCols.Remove(col);
                chessboard[row, col] = false;
            }

            private static void MarkAllAttackedPositions(int row, int col)
            {
                attackedRows.Add(row);
                attackedCols.Add(col);
                chessboard[row, col] = true;
            }

            private static bool CanPlaceQueen(int row, int col)
            {
                if (attackedRows.Contains(row))
                {
                    return false;
                }

                if (attackedCols.Contains(col))
                {
                    return false;
                }

                for (int i = 1; i < Size; i++)
                {
                    if (row - i < 0 || col - i < 0)
                    {
                        break;
                    }


                    if (chessboard[row - i, col - i])
                    {
                        return false;
                    }
                }

                for (int i = 1; i < Size; i++)
                {
                    if (row + i >= Size || col + i >= Size)
                    {
                        break;
                    }


                    if (chessboard[row + i, col + i])
                    {
                        return false;
                    }
                }

                for (int i = 1; i < Size; i++)
                {
                    if (row - i < 0 || col + i >= Size)
                    {
                        break;
                    }


                    if (chessboard[row - i, col + i])
                    {
                        return false;
                    }
                }
                for (int i = 1; i < Size; i++)
                {
                    if (row + i >= Size || col - i < 0)
                    {
                        break;
                    }


                    if (chessboard[row + i, col - i])
                    {
                        return false;
                    }
                }

                return true;
            }

            private static void PrintSolution()
            {
                for (int row = 0; row < Size; row++)
                {
                    for (int col = 0; col < Size; col++)
                    {
                        if (chessboard[row, col])
                        {
                            Console.Write('*');
                        }
                        else
                        {
                            Console.Write('-');
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
