using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var engines = new List<Engine>();
        var enginesCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < enginesCount; i++)
        {
            var engineTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var engineModel = engineTokens[0];
            var enginePower = engineTokens[1];
            var engineDisplacement = "n/a";
            var engineEfficient = "n/a";
            if(engineTokens.Length == 3)
            {
                int trier = 0;
                if (int.TryParse(engineTokens[2], out trier))
                {
                    engineDisplacement = engineTokens[2];
                }
                else
                {
                    engineEfficient = engineTokens[2];
                }
            }
            else if(engineTokens.Length == 4)
            {
                engineDisplacement = engineTokens[2];
                engineEfficient = engineTokens[3];
            }

            engines.Add(new Engine(engineModel, enginePower, engineDisplacement, engineEfficient));
        }

        var cars = new List<Car>();
        var carsCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < carsCount; i++)
        {
            var carTokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var carModel = carTokens[0];
            var engineModel = carTokens[1];           
            var carEngine = engines.Find(e => e.model == engineModel);
            var carWeight = "n/a";
            var carColor = "n/a";
            if(carTokens.Length == 3)
            {
                int trier = 0;
                if(int.TryParse(carTokens[2], out trier))
                {
                    carWeight = carTokens[2];
                }
                else
                {
                    carColor = carTokens[2];
                }
            }
            else if(carTokens.Length == 4)
            {
                carWeight = carTokens[2];
                carColor = carTokens[3];
            }

            cars.Add(new Car(carModel, carEngine, carWeight, carColor));
        }

        cars
            .Select(e =>
            {
                return $"{e.model}:{"\n"}  {e.engine.model}:{"\n"}    Power: {e.engine.power}{"\n"}    Displacement: {e.engine.displacement}{"\n"}    Efficiency: {e.engine.efficiency}{"\n"}  Weight: {e.weight}{"\n"}  Color: {e.color}";
            })
            .ToList()
            .ForEach(Console.WriteLine);
    }
}

