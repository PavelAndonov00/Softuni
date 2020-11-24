using System;
using System.Collections.Generic;
using System.Text;

class Room
{
    public char[][] Matrix { get; set; }

    public Room(int rowsCount)
    {
        Matrix = new char[rowsCount][];

        InitializeMatrix();
    }

    private void InitializeMatrix()
    {
        for (int row = 0; row < Matrix.GetLength(0); row++)
        {
            Matrix[row] = Console.ReadLine().ToCharArray();
        }
    }
}


