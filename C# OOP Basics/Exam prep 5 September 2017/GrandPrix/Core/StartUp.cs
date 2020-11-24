using System;
using System.Linq;

namespace GrandPrix
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var numberOfLaps = int.Parse(Console.ReadLine());
            var trackLength = int.Parse(Console.ReadLine());

            var raceTower = new RaceTower();
            raceTower.SetTrackInfo(numberOfLaps, trackLength);

            var input = Console.ReadLine();
            while (true)
            {
                var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string result = string.Empty;
                var command = tokens[0];
                try
                {
                    switch (command)
                    {
                        case "RegisterDriver":
                            raceTower.RegisterDriver(tokens.Skip(1).ToList());
                            break;
                        case "Leaderboard":
                            result = raceTower.GetLeaderboard();
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "CompleteLaps":
                            result = raceTower.CompleteLaps(tokens.Skip(1).ToList());
                            if(result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Box":
                            raceTower.DriverBoxes(tokens.Skip(1).ToList());
                            break;
                        case "ChangeWeather":
                            raceTower.ChangeWeather(tokens.Skip(1).ToList());
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    if(ae.Message != "")
                    {
                        Console.WriteLine(ae.Message);
                    }
                }

                if (raceTower.IsRaceOver())
                {
                    break;
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(raceTower.GetWinner());

        }
    }
}
