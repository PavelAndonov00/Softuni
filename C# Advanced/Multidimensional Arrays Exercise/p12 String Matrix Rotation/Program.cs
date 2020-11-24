using System;
using System.Collections.Generic;
using System.Linq;

namespace p12_String_Matrix_Rotation
{
    class Program
    {
        static void Main(string[] args)
        {
            var rotationDegrees = int.Parse(String.Concat(Console.ReadLine().ToCharArray().Skip(7).Reverse().Skip(1).Reverse())) % 360;

            var maxLength = int.MinValue;
            var list = new List<string>();
            var input = Console.ReadLine();
            while (input != "END")
            {
                list.Add(input);

                if(input.Length > maxLength)
                {
                    maxLength = input.Length;
                }

                input = Console.ReadLine();
            }

            var matrix = new char[list.Count][];
            for (int r = 0; r < matrix.Length; r++)
            {
                matrix[r] = new char[maxLength];
                matrix[r] = (list[r] + new string(' ', maxLength - list[r].Length)).ToCharArray();
            }
           
            switch (rotationDegrees)
            {
                case 90:
                    matrix = Rotate90(maxLength, matrix);
                    break;
                case 180:
                    matrix = Rotate180(matrix);
                    break;
                case 270:
                    matrix = Rotate180(matrix);
                    matrix = Rotate90(maxLength, matrix);
                    break;
            }

            for (int r = 0; r < matrix.Length; r++)
            {
                for (int c = 0; c < matrix[r].Length; c++)
                {
                    Console.Write(matrix[r][c]);
                }
                Console.WriteLine();
            }
        }

        private static char[][] Rotate90(int maxLength,char[][] matrix)
        {
            var newMatrix = new char[maxLength][];

            for (int r = 0; r < maxLength; r++)
            {
                newMatrix[r] = new char[matrix.Length];
            }

            var counter = 0;
            for (int matrixCol = matrix.Length -1; matrixCol >= 0; matrixCol--, counter++)
            {
                var current = matrix[counter];
                for (int r = 0; r < maxLength; r++)
                {
                    newMatrix[r][matrixCol] = current[r];
                }
            }

            return newMatrix;
        }

        private static char[][] Rotate180(char[][] matrix)
        {
            var newMatrix = new char[matrix.Length][];
            var counter = 0;
            for (int r = matrix.Length - 1; r >= 0; r--, counter++)
            {
                newMatrix[counter] = new char[matrix[r].Length];
                newMatrix[counter] = matrix[r].Reverse().ToArray();
            }

            return newMatrix;
        }

        private static void Rotate270(char[][] matrix)
        {
            
        }
    }
}
