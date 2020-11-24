using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RaceTower
{
    private Track track;
    private List<Driver> drivers;
    private Stack<Driver> dnfDrivers;
    private string weather;
    private TyreFactory tyreFactory;
    private DriverFactory driverFactory;

    public RaceTower()
    {
        this.drivers = new List<Driver>();
        this.dnfDrivers = new Stack<Driver>();
        this.weather = "Sunny";
        this.tyreFactory = new TyreFactory();
        this.driverFactory = new DriverFactory();
    }

    public void SetTrackInfo(int lapsNumber, int trackLength)
    {
        this.track = new Track(lapsNumber, trackLength);
    }

    public void RegisterDriver(List<string> commandArgs)
    {
        this.drivers.Add(driverFactory.CreateDriver(commandArgs.ToList()));
    }

    public void DriverBoxes(List<string> commandArgs)
    {
        var boxReason = commandArgs[0];
        var driverName = commandArgs[1];
        var currentDriver = this.drivers.FirstOrDefault(d => d.Name == driverName);
        if (currentDriver.BeenInBox)
        {
            return;
        }

        var beenInBox = false;

        switch (boxReason)
        {
            case "ChangeTyres":
                currentDriver.Car.Tyre = tyreFactory.CreateTyre(commandArgs.Skip(2).ToList());
                beenInBox = true;
                break;
            case "Refuel":
                double fuelAmount = Double.Parse(commandArgs[2]);
                currentDriver.Car.FuelAmount += fuelAmount;
                beenInBox = true;
                break;
        }

        if (beenInBox)
        {
            currentDriver.BeenInBox = true;
            currentDriver.TotalTime += 20;
        }
    }

    public string GetWinner()
    {
        var winner = this.drivers.OrderBy(d => d.TotalTime).FirstOrDefault();

        return $"{winner.Name} wins the race for {winner.TotalTime:f3} seconds.";
    }

    public string CompleteLaps(List<string> commandArgs)
    {
        var builder = new StringBuilder();

        int numberOfLaps = int.Parse(commandArgs[0]);

        if (numberOfLaps > this.track.RemainingLaps)
        {
            throw new ArgumentException($"There is no time! On lap {this.track.CurrentLap}.");
        }

        for (int i = 0; i < numberOfLaps; i++)
        {
            this.track.CurrentLap++;
            foreach (var driver in this.drivers)
            {
                driver.BeenInBox = false;
                try
                {
                    driver.TotalTime += 60 / (this.track.Length / driver.Speed);
                    driver.Car.FuelAmount -= this.track.Length * driver.FuelConsumptionPerKm;
                    driver.Car.Tyre.Degradate();
                }
                catch (ArgumentException ae)
                {
                    driver.FailureReason = ae.Message;
                }
            }

            var failedDrivers = this.drivers.Where(d => d.FailureReason != null);
            foreach (var failedDriver in failedDrivers)
            {
                this.dnfDrivers.Push(failedDriver);
            }
            this.drivers = this.drivers.Where(d => d.FailureReason == null).ToList();

            this.drivers = this.drivers.OrderByDescending(d => d.TotalTime).ToList();
            for (int index = 0; index < this.drivers.Count - 1; index++)
            {
                var backDriver = this.drivers[index];
                var frontDriver = this.drivers[index + 1];

                var driverTyres = backDriver.Car.Tyre;

                var difference = backDriver.TotalTime - frontDriver.TotalTime;
                if (difference <= 3 && backDriver is AggressiveDriver && driverTyres is UltrasoftTyre)
                {
                    if (this.weather == "Foggy")
                    {
                        backDriver.FailureReason = "Crashed";
                        this.drivers.Remove(backDriver);
                        this.dnfDrivers.Push(backDriver);

                        index--;
                    }
                    else
                    {
                        backDriver.TotalTime -= 3;
                        frontDriver.TotalTime += 3;

                        index++;

                        builder.AppendLine($"{backDriver.Name} has overtaken {frontDriver.Name} on lap {this.track.CurrentLap}.");
                    }
                }
                else if (difference <= 3 && backDriver is EnduranceDriver && driverTyres is HardTyre)
                {
                    if (this.weather == "Rainy")
                    {
                        backDriver.FailureReason = "Crashed";
                        this.drivers.Remove(backDriver);
                        this.dnfDrivers.Push(backDriver);

                        index--;
                    }
                    else
                    {
                        backDriver.TotalTime -= 3;
                        frontDriver.TotalTime += 3;

                        index++;

                        builder.AppendLine($"{backDriver.Name} has overtaken {frontDriver.Name} on lap {this.track.CurrentLap}.");
                    }
                }
                else if (difference <= 2)
                {
                    backDriver.TotalTime -= 2;
                    frontDriver.TotalTime += 2;

                    index++;

                    builder.AppendLine($"{backDriver.Name} has overtaken {frontDriver.Name} on lap {this.track.CurrentLap}.");
                }
            }
        }

        return builder.ToString().Trim();
    }

    public string GetLeaderboard()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Lap {this.track.CurrentLap}/{this.track.TotalLaps}");

        var position = 1;
        this
            .drivers
            .OrderBy(d => d.TotalTime)
            .Concat(this.dnfDrivers)
            .ToList()
            .ForEach(d =>
            {
                if (d.FailureReason != null)
                {
                    builder.AppendLine($"{position++} {d.Name} {d.FailureReason}");
                }
                else
                {
                    builder.AppendLine($"{position++} {d.Name} {d.TotalTime:f3}");
                }
            });

        return builder.ToString().Trim();
    }

    public void ChangeWeather(List<string> commandArgs)
    {
        this.weather = commandArgs[0];
    }

    public bool IsRaceOver()
    {
        return this.track.CurrentLap == this.track.TotalLaps;
    }
}
