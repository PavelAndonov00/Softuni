using System;

namespace P06_Sneaking
{
    class Sneaking
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var room = new Room(n);

            int[] samPosition = GetSamPosition(room);

            var moves = Console.ReadLine().ToCharArray();
            for (int i = 0; i < moves.Length; i++)
            {
                UpdateEnemy(room);

                int[] getEnemy = GetEnemy(room, samPosition);
                if (IfSamDie(room, samPosition, getEnemy))
                {
                    Environment.Exit(0);
                }

                MoveSam(room, samPosition, moves, i);

                getEnemy = GetEnemy(room, samPosition);
                if(IfNikoladzeDie(room, samPosition, getEnemy))
                {
                    Environment.Exit(0);
                }
            }
        }

        private static bool IfNikoladzeDie(Room room, int[] samPosition, int[] getEnemy)
        {
            if (room.Matrix[getEnemy[0]][getEnemy[1]] == 'N' && samPosition[0] == getEnemy[0])
            {
                room.Matrix[getEnemy[0]][getEnemy[1]] = 'X';
                Console.WriteLine("Nikoladze killed!");
                for (int row = 0; row < room.Matrix.Length; row++)
                {
                    for (int col = 0; col < room.Matrix[row].Length; col++)
                    {
                        Console.Write(room.Matrix[row][col]);
                    }
                    Console.WriteLine();
                }
                return true;
            }

            return false;
        }

        private static void MoveSam(Room room, int[] samPosition, char[] moves, int i)
        {
            room.Matrix[samPosition[0]][samPosition[1]] = '.';
            switch (moves[i])
            {
                case 'U':
                    samPosition[0]--;
                    break;
                case 'D':
                    samPosition[0]++;
                    break;
                case 'L':
                    samPosition[1]--;
                    break;
                case 'R':
                    samPosition[1]++;
                    break;
                default:
                    break;
            }
            room.Matrix[samPosition[0]][samPosition[1]] = 'S';
        }

        private static bool IfSamDie(Room room, int[] samPosition, int[] getEnemy)
        {
            if (samPosition[1] < getEnemy[1] && room.Matrix[getEnemy[0]][getEnemy[1]] == 'd' && getEnemy[0] == samPosition[0])
            {
                room.Matrix[samPosition[0]][samPosition[1]] = 'X';
                Console.WriteLine($"Sam died at {samPosition[0]}, {samPosition[1]}");
                for (int row = 0; row < room.Matrix.Length; row++)
                {
                    for (int col = 0; col < room.Matrix[row].Length; col++)
                    {
                        Console.Write(room.Matrix[row][col]);
                    }
                    Console.WriteLine();
                }
                return true;
            }
            else if (getEnemy[1] < samPosition[1] && room.Matrix[getEnemy[0]][getEnemy[1]] == 'b' && getEnemy[0] == samPosition[0])
            {
                room.Matrix[samPosition[0]][samPosition[1]] = 'X';
                Console.WriteLine($"Sam died at {samPosition[0]}, {samPosition[1]}");
                for (int row = 0; row < room.Matrix.Length; row++)
                {
                    for (int col = 0; col < room.Matrix[row].Length; col++)
                    {
                        Console.Write(room.Matrix[row][col]);
                    }
                    Console.WriteLine();
                }
                return true;
            }

            return false;
        }

        private static int[] GetEnemy(Room room, int[] samPosition)
        {
            int[] getEnemy = new int[2];
            for (int j = 0; j < room.Matrix[samPosition[0]].Length; j++)
            {
                if (room.Matrix[samPosition[0]][j] != '.' && room.Matrix[samPosition[0]][j] != 'S')
                {
                    getEnemy[0] = samPosition[0];
                    getEnemy[1] = j;
                }
            }

            return getEnemy;
        }

        private static void UpdateEnemy(Room room)
        {
            for (int row = 0; row < room.Matrix.Length; row++)
            {
                for (int col = 0; col < room.Matrix[row].Length; col++)
                {
                    if (room.Matrix[row][col] == 'b')
                    {
                        if (row >= 0 && row < room.Matrix.Length && col + 1 >= 0 && col + 1 < room.Matrix[row].Length)
                        {
                            room.Matrix[row][col] = '.';
                            room.Matrix[row][col + 1] = 'b';
                            col++;
                        }
                        else
                        {
                            room.Matrix[row][col] = 'd';
                        }
                    }
                    else if (room.Matrix[row][col] == 'd')
                    {
                        if (row >= 0 && row < room.Matrix.Length && col - 1 >= 0 && col - 1 < room.Matrix[row].Length)
                        {
                            room.Matrix[row][col] = '.';
                            room.Matrix[row][col - 1] = 'd';
                        }
                        else
                        {
                            room.Matrix[row][col] = 'b';
                        }
                    }
                }
            }
        }

        private static int[] GetSamPosition(Room room)
        {
            var samPosition = new int[2];
            for (int row = 0; row < room.Matrix.Length; row++)
            {
                for (int col = 0; col < room.Matrix[row].Length; col++)
                {
                    if (room.Matrix[row][col] == 'S')
                    {
                        samPosition[0] = row;
                        samPosition[1] = col;
                    }
                }
            }

            return samPosition;
        }
    }
}
