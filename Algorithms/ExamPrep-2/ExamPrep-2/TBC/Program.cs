using System;

namespace TBC
{
    class Program
    {
        static char[,] matrix;
        static bool[,] visited;
        static void Main(string[] args)
        {
            var rowsCount = int.Parse(Console.ReadLine());
            var colsCount = int.Parse(Console.ReadLine());
            matrix = new char[rowsCount, colsCount];
            visited = new bool[rowsCount, colsCount];
            for (int r = 0; r < rowsCount; r++)
            {
                var row = Console.ReadLine();
                for (int c = 0; c < row.Length; c++)
                {
                    matrix[r, c] = row[c];
                }
            }

            var counter = 0;
            for (int r = 0; r < rowsCount; r++)
            {
                for (int c = 0; c < colsCount; c++)
                {
                    if (matrix[r, c] == 't' && !visited[r, c])
                    {
                        FindPath(r, c);
                        counter++;
                    }
                }
            }

            Console.WriteLine(counter);
        }


        private static void FindPath(int row, int col)
        {
            if (row < 0 || col < 0 ||
                row > matrix.GetLength(0) - 1 ||
                col > matrix.GetLength(1) - 1 ||
                matrix[row, col] == 'd' ||
                visited[row, col])
            {
                return;
            }

            visited[row, col] = true;

            //Vertical
            FindPath(row + 1, col);
            FindPath(row - 1, col);
            //Horizontal
            FindPath(row, col + 1);
            FindPath(row, col - 1);
            // Primary
            FindPath(row + 1, col + 1);
            FindPath(row - 1, col - 1);
            // Secondary
            FindPath(row - 1, col + 1);
            FindPath(row + 1, col - 1);
        }
    }
}
