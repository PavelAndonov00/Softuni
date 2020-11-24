using System;

namespace p02_Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var matrix = new char[n][];
            for (int i = 0; i < n; i++)
            {
                matrix[i] = Console.ReadLine().ToCharArray();
            }

            var removedKnights = 0;
            while (true)
            {
                var maxAttacked = 0;
                var targetRow = 0;
                var targetCol = 0;
                for (int r = 0; r < matrix.Length; r++)
                {
                    for (int c = 0; c < matrix[r].Length; c++)
                    {
                        var attacked = 0;
                        if(matrix[r][c] == 'K')
                        {
                            try
                            {
                                if(matrix[r - 2][c - 1] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {                                
                            }

                            try
                            {
                                if (matrix[r - 2][c + 1] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r + 2][c - 1] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r + 2][c + 1] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r - 1][c - 2] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r + 1][c - 2] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r - 1][c + 2] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                if (matrix[r + 1][c + 2] == 'K')
                                {
                                    attacked++;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }

                        if(attacked > maxAttacked)
                        {
                            maxAttacked = attacked;
                            targetRow = r;
                            targetCol = c;
                        }
                    }
                }

                if(maxAttacked > 0)
                {
                    matrix[targetRow][targetCol] = '0';
                    removedKnights++;
                }
                else
                {
                    Console.WriteLine(removedKnights);
                    Environment.Exit(0);
                }
            }
        }
    }
}
