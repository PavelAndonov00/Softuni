using System;

public class StartUp
{
    static void Main(string[] args)
    {
        var length = double.Parse(Console.ReadLine());
        var width = double.Parse(Console.ReadLine());
        var heigth = double.Parse(Console.ReadLine());

        try
        {
            var box = new Box(length, width, heigth);
            Console.WriteLine("Surface Area - " + box.SurfaceArea.ToString("f2"));
            Console.WriteLine("Lateral Surface Area - " + box.LateralSurfaceArea.ToString("f2"));
            Console.WriteLine("Volume - " + box.Volume.ToString("f2"));
        }
        catch(ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

