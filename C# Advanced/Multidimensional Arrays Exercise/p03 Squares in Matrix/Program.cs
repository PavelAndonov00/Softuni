using System;
using System.Linq;

namespace p03_Squares_in_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsAndColsCount = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rowsCount = rowsAndColsCount[0];
            var colsCount = rowsAndColsCount[1];
            var matrix = new string[rowsCount, colsCount];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var colsStrings = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)                
                    .ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = colsStrings[col];
                }
            }

            var counter = 0;
            for (int row = 0; row < matrix.GetLength(0)-1; row++)
            {                
                for (int col = 0; col < matrix.GetLength(1)-1; col++)
                {
                    if(matrix[row, col] == matrix[row, col+1] && 
                        matrix[row, col + 1] == matrix[row + 1, col] &&
                        matrix[row+1, col] == matrix[row+1, col+1])
                    {
                        counter++;
                    }
                }
            }

            Console.WriteLine(counter);
        }
    }
}
