using System;
using System.Linq;

namespace p07_Lego_Blocks
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsCount = int.Parse(Console.ReadLine());

            var firstJaggedArr = new int[rowsCount][];            
            for (int r = 0; r < rowsCount; r++)
            {
                var row = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                firstJaggedArr[r] = new int[row.Length];
                firstJaggedArr[r] = row;
            }

            var secondJaggedArr = new int[rowsCount][];
            for (int r = 0; r < rowsCount; r++)
            {
                var row = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .Reverse()
                    .ToArray();

                secondJaggedArr[r] = new int[row.Length];
                secondJaggedArr[r] = row;
            }

            var combinedArr = new int[rowsCount][];
            for (int r = 0; r < firstJaggedArr.Length; r++)
            {
                combinedArr[r] = new int[firstJaggedArr[r].Length + secondJaggedArr[r].Length];
                combinedArr[r] = firstJaggedArr[r].Concat(secondJaggedArr[r]).ToArray();
            }

            var totalCols = 0;
            var previousLength = 0;
            var isMatch = true;
            for (int r = 0; r < combinedArr.Length; r++)
            {
                if(previousLength != combinedArr[r].Length && r != 0)
                {
                    isMatch = false;
                }

                totalCols += combinedArr[r].Length;

                previousLength = combinedArr[r].Length;
            }

            if(isMatch)
            {
                for (int r = 0; r < combinedArr.Length; r++)
                {
                    Console.WriteLine("[" + String.Join(", ", combinedArr[r]) +"]");
                }
            }
            else
            {
                Console.WriteLine($"The total number of cells is: {totalCols}");
            }
        
        }
    }
}
