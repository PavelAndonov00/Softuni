using System;
using System.Linq;

namespace p03_Group_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
                .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var jaggedArray = new int[3][];
            var sizes = new int[3];
            foreach (var number in numbers)
            {
                sizes[Math.Abs(number % 3)]++;
            }

            jaggedArray[0] = new int[sizes[0]];
            jaggedArray[1] = new int[sizes[1]];
            jaggedArray[2] = new int[sizes[2]];

            var counter0 = 0;
            var counter1 = 0;
            var counter2 = 0;
            foreach (var number in numbers)
            {
                if(Math.Abs(number % 3) == 0)
                {
                    jaggedArray[0][counter0++] = number;
                }
                else if (Math.Abs(number % 3) == 1)
                {
                    jaggedArray[1][counter1++] = number;
                }
                else
                {
                    jaggedArray[2][counter2++] = number;
                }
            }

            for (int row = 0; row < jaggedArray.Length; row++)
            {
                for (int col = 0; col < jaggedArray[row].Length; col++)
                {
                    Console.Write(jaggedArray[row][col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
