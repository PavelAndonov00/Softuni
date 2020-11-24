using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var cars = new List<Car>();

        var carsCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < carsCount; i++)
        {
            var car = Console.ReadLine().Split();

            var model = car[0];
            var fuelAmount = decimal.Parse(car[1]);
            var fuelConsumption = decimal.Parse(car[2]);

            cars.Add(new Car(model, fuelAmount, fuelConsumption));
        }

        var input = Console.ReadLine();
        while (input != "End")
        {
            var tokens = input.Split().Skip(1).ToArray();

            var model = tokens[0];
            var distance = decimal.Parse(tokens[1]);
            if(cars.Any(e => e.Model == model))
            {
                cars.Find(e => e.Model == model).Travel(distance);
            }

            input = Console.ReadLine();
        }

        cars
            .Select(e => $"{e.Model} {e.FuelAmount:f2} {e.TraveledDistance}")
            .ToList()
            .ForEach(Console.WriteLine);
    }
}
