using System;
using System.Linq;

namespace Socks
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstSocks = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var secondSocks = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            var table = new int[firstSocks.Length + 1, secondSocks.Length + 1];
            for (int r = 1; r <= firstSocks.Length; r++)
            {
                for (int c = 1; c <= secondSocks.Length; c++)
                {
                    if (firstSocks[r - 1] == secondSocks[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            Console.WriteLine(table[firstSocks.Length, secondSocks.Length]);
        }
    }
}
