using System;
using System.Collections.Generic;
using System.Text;

class Car
{
    private string model;
    private decimal fuelAmount;
    private decimal fuelConsumption;
    private decimal traveledDistance;

    public string Model
    {
        get
        {
            return this.model;
        }
        set
        {
            this.model = value;
        }
    }

    public decimal FuelAmount
    {
        get
        {
            return this.fuelAmount;
        }
        set
        {
            this.fuelAmount = value;
        }
    }

    public decimal FuelConsumption
    {
        get
        {
            return this.fuelConsumption;
        }
        set
        {
            this.fuelConsumption = value;
        }
    }

    public decimal TraveledDistance
    {
        get
        {
            return this.traveledDistance;
        }
        set
        {
            this.traveledDistance = value;
        }
    }

    public void Travel(decimal distance)
    {
        var consumption = distance * this.FuelConsumption;
        if(consumption > this.FuelAmount)
        {
            Console.WriteLine("Insufficient fuel for the drive");
        }
        else
        {
            this.FuelAmount -= consumption;
            this.traveledDistance += distance;
        }
    }

    public Car()
    {
        this.traveledDistance = 0;
    }

    public Car(string model, decimal fuelAmount, decimal fuelConsumption) : this()
    {
        this.Model = model;
        this.FuelAmount = fuelAmount;
        this.FuelConsumption = fuelConsumption;
    }
}

