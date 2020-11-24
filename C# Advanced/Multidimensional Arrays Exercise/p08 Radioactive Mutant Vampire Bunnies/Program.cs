using System;
using System.Linq;

namespace p08_Radioactive_Mutant_Vampire_Bunnies
{
    class Program
    {
        static void Main(string[] args)
        {
            var dimensions = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var matrix = new char[dimensions[0]][];
            for (int r = 0; r < matrix.Length; r++)
            {
                matrix[r] = new char[dimensions[1]];
            }

            var playerRow = 0;
            var playerCol = 0;
            for (int r = 0; r < dimensions[0]; r++)
            {
                matrix[r] = Console.ReadLine().ToCharArray();
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if (matrix[r][c] == 'P')
                    {
                        playerRow = r;
                        playerCol = c;
                    }
                }
            }

            var directions = Console.ReadLine();

            foreach (var direction in directions)
            {
                switch (direction)
                {
                    case 'U':
                        if (playerRow - 1 >= 0)
                        {
                            if (matrix[playerRow - 1][playerCol] == 'B')
                            {
                                playerRow--;
                                Finish(matrix, playerRow, playerCol, 'P');
                                return;
                            }
                            else
                            {
                                matrix[playerRow][playerCol] = '.';
                                playerRow--;
                                matrix[playerRow][playerCol] = 'P';
                            }
                        }
                        else
                        {
                            Finish(matrix, playerRow, playerCol, 'B');
                            return;
                        }
                        break;
                    case 'R':
                        if (playerCol + 1 < matrix[0].Length)
                        {
                            if (matrix[playerRow][playerCol + 1] == 'B')
                            {
                                playerCol++;
                                Finish(matrix, playerRow, playerCol, 'P');
                                return;
                            }
                            else
                            {
                                matrix[playerRow][playerCol] = '.';
                                playerCol++;
                                matrix[playerRow][playerCol] = 'P';
                            }
                        }
                        else
                        {
                            Finish(matrix, playerRow, playerCol, 'B');
                            return;
                        }
                        break;
                    case 'D':
                        if (playerRow + 1 < matrix.Length)
                        {
                            if (matrix[playerRow + 1][playerCol] == 'B')
                            {
                                playerRow++;
                                Finish(matrix, playerRow, playerCol, 'P');
                                return;
                            }
                            else
                            {
                                matrix[playerRow][playerCol] = '.';
                                playerRow++;
                                matrix[playerRow][playerCol] = 'P';
                            }
                        }
                        else
                        {
                            Finish(matrix, playerRow, playerCol, 'B');
                            return;
                        }
                        break;
                    case 'L':
                        if (playerCol - 1 >= 0)
                        {
                            if (matrix[playerRow][playerCol - 1] == 'B')
                            {
                                playerCol--;
                                Finish(matrix, playerRow, playerCol, 'P');
                                return;
                            }
                            else
                            {
                                matrix[playerRow][playerCol] = '.';
                                playerCol--;
                                matrix[playerRow][playerCol] = 'P';
                            }
                        }
                        else
                        {
                            Finish(matrix, playerRow, playerCol, 'B');
                            return;
                        }
                        break;
                }

                if (!SpreadBunnies(matrix, playerRow, playerCol, false))
                {
                    return;
                }
            }

        }

        private static bool SpreadBunnies(char[][] matrix, int playerRow, int playerCol, bool isPlayerDead)
        {
            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if (matrix[r][c] == 'B')
                    {
                        if (r - 1 >= 0)
                        {
                            if (matrix[r - 1][c] == 'P' && !isPlayerDead)
                            {
                                Finish(matrix, playerRow, playerCol, 'P');
                                return false;
                            }
                            else if (matrix[r - 1][c] != 'B')
                            {
                                matrix[r - 1][c] = 'b';
                            }
                        }

                        if (r + 1 < matrix.Length)
                        {
                            if (matrix[r + 1][c] == 'P' && !isPlayerDead)
                            {
                                Finish(matrix, playerRow, playerCol, 'P');
                                return false;
                            }
                            else if (matrix[r + 1][c] != 'B')
                            {
                                matrix[r + 1][c] = 'b';
                            }
                        }

                        if (c - 1 >= 0)
                        {
                            if (matrix[r][c - 1] == 'P' && !isPlayerDead)
                            {
                                Finish(matrix, playerRow, playerCol, 'P');
                                return false;
                            }
                            else if (matrix[r][c - 1] != 'B')
                            {
                                matrix[r][c - 1] = 'b';
                            }
                        }

                        if (c + 1 < matrix[r].Length)
                        {
                            if (matrix[r][c + 1] == 'P' && !isPlayerDead)
                            {
                                Finish(matrix, playerRow, playerCol, 'P');
                                return false;
                            }
                            else if (matrix[r][c + 1] != 'B')
                            {
                                matrix[r][c + 1] = 'b';
                            }
                        }
                    }
                }
            }

            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if (matrix[r][c] == 'b')
                    {
                        matrix[r][c] = 'B';
                    }
                }
            }

            return true;
        }

        private static void Finish(char[][] matrix, int playerRow, int playerCol, char whoDied)
        {
            var isFound = false;
            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    if(matrix[r][c] == 'P')
                    {
                        matrix[r][c] = '.';
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }
            }

            var resultString = "";
            if (whoDied == 'P')
            {
                resultString = "dead";
                SpreadBunnies(matrix, playerRow, playerRow, true);
            }
            else
            {
                resultString = "won";
                SpreadBunnies(matrix, playerRow, playerRow, false);
            }

            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    Console.Write(matrix[r][c]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"{resultString}: {playerRow} {playerCol}");
        }
    }
}
