using System;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var rectanglePoints = Console.ReadLine()
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(double.Parse)
            .ToArray();
        var rectangle = CreateRectangle(rectanglePoints);

        var numberOfPoints = int.Parse(Console.ReadLine());
        for (int i = 0; i < numberOfPoints; i++)
        {
            var pointCoordinates = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            Console.WriteLine(PointIsInRect(pointCoordinates, rectangle));
        }
    }

    private static bool PointIsInRect(double[] pointCoordinates, Rectangle rectangle)
    {
        var point = new Point(pointCoordinates[0], pointCoordinates[1]);
        return rectangle.Contains(point);
    }

    private static Rectangle CreateRectangle(double[] rectanglePoints)
    {
        return new Rectangle(rectanglePoints[0], rectanglePoints[1],
            rectanglePoints[2], rectanglePoints[3]);
    }
}

