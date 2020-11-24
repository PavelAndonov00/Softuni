using System;
using System.Collections.Generic;
using System.Text;

public class Box
{
    private double Length { get; set; }
    private double Width { get; set; }
    private double Height { get; set; }

    public Box(double length, double width, double height)
    {
        this.Length = length;
        this.Width = width;
        this.Height = height;
    }

    private double CalcVolume()
    {
        return this.Length * this.Width * this.Height;
    }

    public double Volume
    {
        get
        {
            return CalcVolume();
        }
    }

    private double CalcLateralSurfaceArea()
    {
        return 2 * this.Length * this.Height + 2 * this.Width * this.Height;
    }

    public double LateralSurfaceArea
    {
        get
        {
            return CalcLateralSurfaceArea();
        }
    }

    private double CalcSurfaceArea()
    {
        return 2 * this.Length * this.Width + 2 * this.Length * this.Height + 2 * this.Width * this.Height;
    }

    public double SurfaceArea
    {
        get
        {
            return CalcSurfaceArea();
        }
    }
}

