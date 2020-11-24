using System;
using System.Linq;

namespace p10_The_Heigan_Dance
{
    class Program
    {
        static void Main(string[] args)
        {
            var playerDamage = double.Parse(Console.ReadLine());

            var heiganHealth = 3000000.0;
            var playerHealth = 18500;
            var playerRow = 7;
            var playerCol = 7;
            var isInfected = false;
            var spell = "";
            while (true)
            {
                var tokens = Console.ReadLine()
                    .Split(" ")
                    .ToArray();

                heiganHealth -= playerDamage;
                if (heiganHealth <= 0)
                {
                    Console.WriteLine("Heigan: Defeated!");
                    Console.WriteLine($"Player: {playerHealth}");
                    Console.WriteLine($"Final position: {playerRow}, {playerCol}");
                    break;
                }

                if (isInfected)
                {
                    playerHealth -= 3500;
                    isInfected = false;

                    if (playerHealth <= 0)
                    {
                        Console.WriteLine($"Heigan: {heiganHealth:f2}");
                        if (spell == "Eruption")
                        {
                            Console.WriteLine("Player: Killed by Eruption");
                        }
                        else
                        {
                            Console.WriteLine("Player: Killed by Plague Cloud");
                        }
                        Console.WriteLine($"Final position: {playerRow}, {playerCol}");
                        break;
                    }
                }

                spell = tokens[0];
                var targetRow = int.Parse(tokens[1]);
                var targetCol = int.Parse(tokens[2]);
                
                var isInRange = false;
                for (int r = Math.Max(0, targetRow - 1); r <= Math.Min(14, targetRow + 1); r++)
                {
                    for (int c = Math.Max(0, targetCol - 1); c <= Math.Min(14, targetCol + 1); c++)
                    {
                        if (r == playerRow && c == playerCol)
                        {
                            isInRange = true;
                            break;
                        }
                    }
                    if (isInRange)
                    {
                        break;
                    }
                }

                if (isInRange)
                {
                    var up = true; var right = true; var down = true; var left = true;
                    for (int r = Math.Max(0, targetRow - 1); r <= Math.Min(14, targetRow + 1); r++)
                    {
                        for (int c = Math.Max(0, targetCol - 1); c <= Math.Min(14, targetCol + 1); c++)
                        {
                            if (Math.Max(0, playerRow - 1) == r && playerCol == c && up)
                            {
                                up = false;
                            }

                            if (playerRow == r && Math.Min(14, playerCol + 1) == c && right)
                            {
                                right = false;
                            }

                            if (Math.Min(14, playerRow + 1) == r && playerCol == c && down)
                            {
                                down = false;
                            }

                            if (playerRow == r && Math.Max(0, playerCol - 1) == c && left)
                            {
                                left = false;
                            }
                        }
                    }

                    if (up)
                    {
                        playerRow--;
                    }
                    else if (right)
                    {
                        playerCol++;
                    }
                    else if (down)
                    {
                        playerRow++;
                    }
                    else if (left)
                    {
                        playerCol--;
                    }
                    else
                    {
                        if (spell == "Cloud")
                        {
                            playerHealth -= 3500;
                            isInfected = true;
                        }
                        else
                        {
                            playerHealth -= 6000;
                        }
                    }
                }

                if (playerHealth <= 0)
                {
                    Console.WriteLine($"Heigan: {heiganHealth:f2}");
                    if (spell == "Eruption")
                    {
                        Console.WriteLine("Player: Killed by Eruption");
                    }
                    else
                    {
                        Console.WriteLine("Player: Killed by Plague Cloud");
                    }
                    Console.WriteLine($"Final position: {playerRow}, {playerCol}");
                    break;
                }
            }
        }
    }
}
