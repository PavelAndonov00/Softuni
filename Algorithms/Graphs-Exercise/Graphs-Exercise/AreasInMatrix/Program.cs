using System;
using System.Collections.Generic;
using System.Linq;

namespace AreasInMatrix
{
    class Program
    {
        static char[][] matrix;
        static bool[][] visited;
        static Dictionary<char, int> areasCount;
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var m = int.Parse(Console.ReadLine());

            matrix = new char[n][];
            visited = new bool[n][];
            ReadInput(n, m);

            areasCount = new Dictionary<char, int>();
            ExtractConnectedAreas(n, m);
            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine("Areas: " + areasCount.Sum(ac => ac.Value));
            Console.WriteLine(string.Join("\r\n", areasCount
                                                    .OrderBy(ac => ac.Key)
                                                    .Select(ac => $"Letter '{ac.Key}' -> {ac.Value}")));
        }

        private static void ExtractConnectedAreas(int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < m; k++)
                {
                    if (!visited[i][k])
                    {
                        var letter = matrix[i][k];
                        DFS(i, k, letter);
                        if (!areasCount.ContainsKey(letter))
                        {
                            areasCount[letter] = 0;
                        }

                        areasCount[letter]++;
                    }
                }
            }
        }

        private static void ReadInput(int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().ToCharArray();
                matrix[i] = input;
                visited[i] = new bool[m];
            }
        }

        private static void DFS(int row, int col, char searchedLetter)
        {
            if (IsOutOfBounds(row, col) || visited[row][col]
               || matrix[row][col] != searchedLetter)
            {
                return;
            }

            visited[row][col] = true;
            DFS(row, col + 1, searchedLetter);
            DFS(row + 1, col, searchedLetter);
            DFS(row, col - 1, searchedLetter);
            DFS(row - 1, col, searchedLetter);
        }

        private static bool IsOutOfBounds(int row, int col)
        {
            if (row > matrix.Length - 1 || row < 0 ||
                col > matrix[0].Length - 1 || col < 0)
            {
                return true;
            }

            return false;
        }
    }
}
