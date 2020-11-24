using System;
using System.Linq;

class JediGalaxy
{
    static void Main()
    {
        int[] dimensions = Console.ReadLine()
            .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        int rowsCount = dimensions[0];
        int colsCount = dimensions[1];

        var field = new Matrix(rowsCount, colsCount);

        string command = "";
        var ivos = new Good();
        while ((command = Console.ReadLine()) != "Let the Force be with you")
        {           
            int[] evilStartCoordinates = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            var evil = new Evil(evilStartCoordinates[0], evilStartCoordinates[1]);
            field = evil.Destroy(field);

            int[] ivosStartCoordinates = command
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            if(ivos.Sum == -1)
            {
                ivos = new Good(ivosStartCoordinates[0], ivosStartCoordinates[1]);
            }
            else
            {
                ivos.Row = ivosStartCoordinates[0];
                ivos.Col = ivosStartCoordinates[1];
            }
            ivos.MoveAndSum(field);
        }

        Console.WriteLine(ivos.Sum);
    }    
}

