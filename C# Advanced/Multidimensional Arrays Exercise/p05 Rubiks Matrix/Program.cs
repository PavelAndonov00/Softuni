using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_Rubiks_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            // read rows and cols count from the console
            var rowsAndCols = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var rowsCount = rowsAndCols[0];
            var colsCount = rowsAndCols[1];
            var matrix = new int[rowsCount, colsCount];
            var counter = 1;
            
            // fill the matrix with increasing integers starting from 1
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    matrix[row, col] = counter++;
                }
            }

            // read count of operaton and execute them
            var operationsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < operationsCount; i++)
            {
                var operation = Console.ReadLine()
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var direction = operation[1];

                if (direction == "down" || direction == "up")
                {
                    var col = int.Parse(operation[0]);
                    // dividing moves by cols length to save memory and time
                    var moves = int.Parse(operation[2]) % matrix.GetLength(1);

                    // copy read col to rotate
                    var copiedCol = CopyColInArr(matrix, col);
                    switch (direction)
                    {
                        case "down":
                            var firstPartToRight = copiedCol.Skip(copiedCol.Length - moves);
                            var secondPartToRight = copiedCol.Take(copiedCol.Length - moves);

                            copiedCol = firstPartToRight.Concat(secondPartToRight).ToArray();
                            break;
                        case "up":
                            var firstPartToLeft = copiedCol.Take(moves);
                            var secondPartToLeft = copiedCol.Skip(moves);

                            copiedCol = secondPartToLeft.Concat(firstPartToLeft).ToArray();
                            break;
                    }

                    // update the matrix with the rotated col
                    for (int matrixRow = 0; matrixRow < matrix.GetLength(0); matrixRow++)
                    {
                        matrix[matrixRow, col] = copiedCol[matrixRow];
                    }
                }
                else
                {
                    var row = int.Parse(operation[0]);
                    // dividing moves by rows length to save memory and time
                    var moves = int.Parse(operation[2]) % matrix.GetLength(0);

                    // copy read row to rotate 
                    var copiedRow = CopyRowInArray(matrix, row);
                    switch (direction)
                    {
                        case "right":
                            var firstPartToRight = copiedRow.Skip(copiedRow.Length - moves);
                            var secondPartToRight = copiedRow.Take(copiedRow.Length - moves);

                            copiedRow = firstPartToRight.Concat(secondPartToRight).ToArray();
                            break;
                        case "left":
                            var firstPartToLeft = copiedRow.Take(moves);
                            var secondPartToLeft = copiedRow.Skip(moves);

                            copiedRow = secondPartToLeft.Concat(firstPartToLeft).ToArray();
                            break;
                    }

                    // update the matrix with the rotated row
                    for (int matrixCol = 0; matrixCol < matrix.GetLength(1); matrixCol++)
                    {
                        matrix[row, matrixCol] = copiedRow[matrixCol];
                    }
                }
            }

            // check if the first number have to swap
            if (matrix[0, 0] != 1)
            {
                var foundNumber = FindIndexOfNumber(matrix, 1);
                var foundNumberRow = foundNumber[0];
                var foundNumberCol = foundNumber[1];

                var current = matrix[0, 0];

                matrix[foundNumberRow, foundNumberCol] = current;
                matrix[0, 0] = 1;
                PrintSwapedIndexes(new int[] { 0, 0 }, foundNumber);
            }
            else
            {
                Console.WriteLine("No swap required");
            }

            var currentRow = 0;
            var currentCol = 0;
            // fill list with all numbers to check for every number from the matrix
            var filledList = FillListWithAllNumbers(matrix);
            for (int i = 0; i < filledList.Count-1; i++)
            {
                // every notation goes right 
                currentCol++;
                if (currentCol > matrix.GetLength(1)-1)
                {
                    currentRow++;
                    currentCol = 0;
                }
        
                var current = filledList[i];
                var next = filledList[i + 1];

                if (next != current + 1)
                {
                    // look for finding number index
                    var foundNumberIndexes = FindIndexOfNumber(matrix, current + 1);
                    var foundNumberRow = foundNumberIndexes[0];
                    var foundNumberCol = foundNumberIndexes[1];

                    // update matrix
                    matrix[foundNumberRow, foundNumberCol] = next;
                    matrix[currentRow, currentCol] = current + 1;

                    // print indexes
                    PrintSwapedIndexes(new int[] { currentRow, currentCol }, foundNumberIndexes);

                    // update list
                    filledList[i + 1] = current + 1;
                    filledList[foundNumberRow * matrix.GetLength(1) + foundNumberCol] = matrix[foundNumberRow, foundNumberCol];
                }
                else
                {
                    Console.WriteLine("No swap required");
                }
            }
        }

        private static List<int> FillListWithAllNumbers(int[,] matrix)
        {
            var list = new List<int>();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    list.Add(matrix[row, col]);
                }
            }

            return list;
        }

        private static void PrintSwapedIndexes(int[] firstIndexes, int[] secondIndexes)
        {
            Console.WriteLine($"Swap ({firstIndexes[0]}, {firstIndexes[1]}) " +
                $"with ({ secondIndexes[0]}, {secondIndexes[1]})");
        }

        private static int[] FindIndexOfNumber(int[,] matrix, int number)
        {
            var result = new int[2];
            var isFound = false;
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == number)
                    {
                        result[0] = row;
                        result[1] = col;
                        isFound = true;
                        break;
                    }
                }                

                if(isFound)
                {
                    break;
                }
            }

            return result;
        }

        private static int[] CopyColInArr(int[,] matrix, int col)
        {
            var arr = new int[matrix.GetLength(0)];
            for (int matrixRow = 0; matrixRow < matrix.GetLength(0); matrixRow++)
            {
                arr[matrixRow] = matrix[matrixRow, col];
            }

            return arr;
        }

        private static int[] CopyRowInArray(int[,] matrix, int row)
        {
            var arr = new int[matrix.GetLength(1)];
            for (int matrixCol = 0; matrixCol < matrix.GetLength(1); matrixCol++)
            {
                arr[matrixCol] = matrix[row, matrixCol];
            }

            return arr;
        }
    }
}
