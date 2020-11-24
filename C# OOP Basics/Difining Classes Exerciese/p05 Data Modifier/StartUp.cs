using System;


class StartUp
{
    static void Main(string[] args)
    {
        var date1 = Console.ReadLine();
        var date2 = Console.ReadLine();

        var dataModifier = new DataModifier();
        Console.WriteLine(dataModifier.CalculateDifference(date1, date2));
    }
}

