using System;
using System.Collections.Generic;
using System.Text;

class Matrix
{
    public int[,] matrix { get; set; }
    public int RowsCount { get; }
    public int ColsCount { get; }

    public Matrix(int rowsCount, int colsCount)
    {
        matrix = new int[rowsCount, colsCount];

        RowsCount = rowsCount;
        ColsCount = colsCount;

        FillMatrix();
    }

    private void FillMatrix()
    {
        int value = 0;
        for (int r = 0; r < matrix.GetLength(0); r++)
        {
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                matrix[r, c] = value++;
            }
        }
    }

    public bool IsInside(int row, int col)
    {
        return row >= 0 && row < RowsCount && col >= 0 && col < ColsCount;
    }
}

