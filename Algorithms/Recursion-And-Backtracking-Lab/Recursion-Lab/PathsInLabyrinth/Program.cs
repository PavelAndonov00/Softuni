using System;
using System.Collections.Generic;
using System.Linq;

namespace PathsInLabyrinth
{
    class Program
    {

        static char[,] matrix;
        static int[,] visited;
        static List<char> path = new List<char>();
        static void Main(string[] args)
        {
            var r = int.Parse(Console.ReadLine());
            var c = int.Parse(Console.ReadLine());
            matrix = new char[r, c];
            visited = new int[r, c];
            PopulateMatrix(matrix);

            FindPaths(0, 0, 'S');
        }

        private static void FindPaths(int row, int col, char direction)
        {
            if(IsOutOfBounds(row, col))
            {
                return;
            }

            path.Add(direction);
            if(IsExit(row, col))
            {
                PrintPath(path);
            }
            else if(!IsVisited(row, col) && IsFree(row, col))
            {
                Mark(row, col);
                FindPaths(row, col + 1, 'R');
                FindPaths(row + 1, col, 'D');
                FindPaths(row, col - 1, 'L');
                FindPaths(row - 1, col, 'U');
                UnMark(row, col);
            }

            path.RemoveAt(path.Count - 1);
        }

        private static void UnMark(int row, int col)
        {
            visited[row, col] = -1;
        }

        private static void Mark(int row, int col)
        {
            visited[row, col] = 1;
        }

        private static bool IsVisited(int row, int col)
        {
            return visited[row, col] != -1;
        }

        private static bool IsFree(int row, int col)
        {
            return matrix[row, col] == '-';
        }

        private static void PrintPath(List<char> path)
        {
            Console.WriteLine(string.Join("", path.Skip(1)));
        }

        private static bool IsExit(int row, int col)
        {
            if(matrix[row, col] == 'e')
            {
                return true;
            }

            return false;
        }

        private static bool IsOutOfBounds(int row, int col)
        {
            return row < 0 || row > matrix.GetLength(0) - 1 || col < 0 || col > matrix.GetLength(1) - 1;
        }

        private static void PopulateMatrix(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var input = Console.ReadLine();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    visited[row, col] = -1;
                    matrix[row, col] = input[col];
                }
            }
        }
    }
}
