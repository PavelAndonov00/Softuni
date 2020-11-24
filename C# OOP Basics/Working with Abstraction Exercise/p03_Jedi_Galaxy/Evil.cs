using System;
using System.Collections.Generic;
using System.Text;

class Evil
{
    public int Row { get; set; }
    public int Col { get; set; }

    public Evil(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public Matrix Destroy(Matrix field)
    {
        while (Row >= 0 && Col >= 0)
        {
            if (field.IsInside(Row, Col))
            {
                field.matrix[Row, Col] = 0;
            }
            Row--;
            Col--;
        }

        return field;
    }
}
