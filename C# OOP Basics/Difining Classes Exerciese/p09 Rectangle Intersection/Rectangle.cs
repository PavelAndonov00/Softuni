using System;
using System.Collections.Generic;
using System.Text;

class Rectangle
{
    public string id { get; set; }
    public double width { get; set; }
    public double height { get; set; }
    public double topLeftX { get; set; }
    public double topLeftY { get; set; }

    public Rectangle(string id, double width, double height, double topLeftX, double topLeftY)
    {
        this.id = id;
        this.width = width;
        this.height = height;
        this.topLeftX = topLeftX;
        this.topLeftY = topLeftY;
    }

    public string isIntersect(Rectangle rect)
    {
        var rect1BottomRightX = this.topLeftX + this.width;
        var rect1BottomRightY = this.topLeftY + this.height;

        var rect2BottomRightX = rect.topLeftX + rect.width;
        var rect2BottomRightY = rect.topLeftY + rect.height;

        if ((this.topLeftX > rect2BottomRightX) || (rect1BottomRightX < rect.topLeftX) || (this.topLeftY > rect2BottomRightY) ||
          (rect1BottomRightY < rect.topLeftY))
        {
            return "false";
        }        

        return "true";
    }
}

