using System;
using System.Linq;

namespace p11_Parking_System
{
    class Program
    {
        static void Main(string[] args)
        {
            var rowsAndColsCount = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var rowsCount = rowsAndColsCount[0];
            var colsCount = rowsAndColsCount[1];            
            var matrix = new long[rowsCount, colsCount];                
           
            var input = Console.ReadLine();
            while (input != "stop")
            {
                var tokens = input
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
            
                var entryRow = tokens[0];
                var parkingSpotRow = tokens[1];
                var parkingSpotCol = tokens[2];
            
                var steps = 1;
            
                if (matrix[parkingSpotRow, parkingSpotCol] == 0)
                {
                    matrix[parkingSpotRow, parkingSpotCol] = 1;
                    steps += Math.Abs(entryRow - parkingSpotRow) + parkingSpotCol;
                    Console.WriteLine(steps);                    
                }
                else
                {
                    var isParked = false;
                    for (int c = 1; c < matrix.GetLength(1); c++)
                    {
                        if (parkingSpotCol - c > 0)
                        {
                            if (matrix[parkingSpotRow, parkingSpotCol - c] == 0)
                            {
                                matrix[parkingSpotRow, parkingSpotCol - c] = 1;
                                steps += Math.Abs(entryRow - parkingSpotRow) + parkingSpotCol - c;
                                isParked = true;
                                break;
                            }
                        }
            
                        if (parkingSpotCol + c < matrix.GetLength(1))
                        {
                            if (matrix[parkingSpotRow, parkingSpotCol + c] == 0)
                            {
                                matrix[parkingSpotRow, parkingSpotCol + c] = 1;
                                steps += Math.Abs(entryRow - parkingSpotRow) + parkingSpotCol + c;
                                isParked = true;
                                break;
                            }
                        }
            
            
                    }
            
                    if (isParked)
                    {
                        Console.WriteLine(steps);
                    }
                    else
                    {
                        Console.WriteLine($"Row {parkingSpotRow} full");
                    }
                }
            
                input = Console.ReadLine();
            }
        }
    }
}
