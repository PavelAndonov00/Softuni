using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var rectangles = new List<Rectangle>();

        var nAndM = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray(); ;
        var n = nAndM[0];
        var m = nAndM[1];

        for (int i = 0; i < n; i++)
        {
            var tokens = Console.ReadLine()
                .Split();

            var id = tokens[0];
            var x = double.Parse(tokens[1]);
            var y = double.Parse(tokens[2]);
            var topLeftX = double.Parse(tokens[3]);
            var topLeftY = double.Parse(tokens[4]);

            rectangles.Add(new Rectangle(id, x, y, topLeftX, topLeftY));
        }

        for (int i = 0; i < m; i++)
        {
            var ids = Console.ReadLine().Split();

            var id1 = ids[0];
            var id2 = ids[1];

            var rectangle1 = rectangles.Find(r => r.id == id1);
            var rectangle2 = rectangles.Find(r => r.id == id2);

            Console.WriteLine(rectangle1.isIntersect(rectangle2));
        }
    }
}

