using System;
using System.Linq;

namespace p02_Diagonal_Difference
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowCount = int.Parse(Console.ReadLine());
           
            var primarySum = 0;
            var secondarySum = 0;
            for (int row = 0; row < rowCount; row++)
            {
                var colNumbers = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                
                    primarySum += colNumbers[row];
                    secondarySum += colNumbers[colNumbers.Length - 1 - row];
            }           
          
            Console.WriteLine(Math.Abs(primarySum - secondarySum));
        }
    }
}
