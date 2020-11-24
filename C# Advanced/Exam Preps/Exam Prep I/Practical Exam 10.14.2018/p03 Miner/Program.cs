using System;
using System.Linq;

namespace p03_Miner
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsCount = int.Parse(Console.ReadLine());

            var directions = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var matrix = new char[rowsCount][];
            var minerRow = 0;
            var minerCol = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                matrix[i] = Console.ReadLine()
                    .Split(new string[] { " " },StringSplitOptions.RemoveEmptyEntries)
                    .Select(char.Parse)
                    .ToArray();
                for (int c = 0; c < matrix[i].Length; c++)
                {
                    if(matrix[i][c] == 's')
                    {
                        minerRow = i;
                        minerCol = c;
                    }
                }
            }

            foreach (var direction in directions)
            {
                if (direction == "left")
                {
                    if (isInside(matrix, minerRow, minerCol - 1))
                    {
                        minerCol--;
                        var current = matrix[minerRow][minerCol];

                        switch (current)
                        {
                            case 'e':
                                Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
                                Environment.Exit(0);
                                break;
                            case 'c':
                                matrix[minerRow][minerCol] = '*';
                                break;
                        }
                    }
                }
                else if (direction == "right")
                {
                    if (isInside(matrix, minerRow, minerCol + 1))
                    {
                        minerCol++;
                        var current = matrix[minerRow][minerCol];

                        switch (current)
                        {
                            case 'e':                               
                                Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
                                Environment.Exit(0);
                                break;
                            case 'c':
                                matrix[minerRow][minerCol] = '*';
                                break;
                        }
                    }
                }
                else if (direction == "up")
                {
                    if (isInside(matrix, minerRow - 1, minerCol))
                    {
                        minerRow--;
                        var current = matrix[minerRow][minerCol];

                        switch (current)
                        {
                            case 'e':
                                Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
                                Environment.Exit(0);
                                break;
                            case 'c':
                                matrix[minerRow][minerCol] = '*';
                                break;
                        }
                    }
                }
                else if (direction == "down")
                {
                    if (isInside(matrix, minerRow + 1, minerCol))
                    {
                        minerRow++;
                        var current = matrix[minerRow][minerCol];

                        switch (current)
                        {
                            case 'e':
                                Console.WriteLine($"Game over! ({minerRow}, {minerCol})");
                                Environment.Exit(0);
                                break;
                            case 'c':
                                matrix[minerRow][minerCol] = '*';
                                break;
                        }
                    }
                }

                if (!isThereCoals(matrix))
                {
                    Console.WriteLine($"You collected all coals! ({minerRow}, {minerCol})");
                    Environment.Exit(0);
                }
            }

            var coalsLeft = 0;
            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if(matrix[r][c] == 'c')
                    {
                        coalsLeft++;
                    }
                }
            }
            Console.WriteLine($"{coalsLeft} coals left. ({minerRow}, {minerCol})");
        }

        private static bool isThereCoals(char[][] matrix)
        {
            for (int r = 0; r < matrix.Length; r++)
            {
                if (matrix[r].Contains('c'))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool isInside(char[][] matrix, int minerRow, int minerCol)
        {
            return minerRow >= 0 && minerRow < matrix.Length && minerCol >= 0 && minerCol < matrix.Length;
        }
    }
}
