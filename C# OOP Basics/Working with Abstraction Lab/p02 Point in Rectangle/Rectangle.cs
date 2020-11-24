using System;
using System.Collections.Generic;
using System.Text;

class Rectangle
{
    public Point topLeft { get; set; }
    public Point bottomRight { get; set; }

    public Rectangle(double x1, double y1, double x2, double y2)
    {
        topLeft = new Point(x1, y1);
        bottomRight = new Point(x2, y2);
    }

    public bool Contains(Point point)
    {
        if(point.X >= topLeft.X && point.X <= bottomRight.X &&
            point.Y >= topLeft.Y && point.Y <= bottomRight.Y)
        {
            return true;
        }

        return false;
    }
}

