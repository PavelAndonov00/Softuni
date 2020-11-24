using System;
using System.Collections.Generic;
using System.Text;

class Good
{
    public int Row { get; set; }
    public int Col { get; set; }
    public long Sum { get; set; }

    public Good()
    {
        Sum = -1;
    }

    public Good(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public void MoveAndSum(Matrix field)
    {
        while (Row >= 0 && Col < field.ColsCount)
        {
            if (field.IsInside(Row, Col))
            {
                Sum += field.matrix[Row, Col];
            }

            Col++;
            Row--;
        }
    }
}

