using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var cars = new List<Car>();

        var carsCount = int.Parse(Console.ReadLine());
        var cargoType = "";
        for (int i = 0; i < carsCount; i++)
        {
            var input = Console.ReadLine().Split();

            var carModel = input[0];
            var enginePower = int.Parse(input[2]);
            cargoType = input[4];

            var tirePressures = new List<double>()
            {
                double.Parse(input[5]),
                double.Parse(input[7]),
                double.Parse(input[9]),
                double.Parse(input[11])
            };

            cars.Add(new Car(carModel, enginePower, cargoType, tirePressures));
        }

        cargoType = Console.ReadLine();

        var matchedWithConditionsCars = new List<string>();
        if (cargoType == "fragile")
        {
            matchedWithConditionsCars = cars
           .Where(e => e.CargoType == cargoType)
           .Where(e => e.TirePressures.Any(t => t < 1))
           .Select(e => e.Model)
           .ToList();
        }
        else
        {
            matchedWithConditionsCars = cars
           .Where(e => e.CargoType == cargoType)
           .Where(e => e.EnginePower > 250)
           .Select(e => e.Model)
           .ToList();
        }


        Console.WriteLine(String.Join("\n", matchedWithConditionsCars));
    }
}

