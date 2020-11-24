using System;

namespace p04_Pascal_Triangle
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = long.Parse(Console.ReadLine());

            var jaggedArray = new long[rows][];
            for (int row = 0; row < jaggedArray.Length; row++)
            {
                jaggedArray[row] = new long[row + 1];
                jaggedArray[row][0] = 1;
                jaggedArray[row][jaggedArray[row].Length - 1] = 1;
                for (int col = 1; col < jaggedArray[row].Length - 1; col++)
                {
                    jaggedArray[row][col] = jaggedArray[row - 1][col - 1] + jaggedArray[row - 1][col];
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
