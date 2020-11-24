using System;
using System.Collections.Generic;
using System.Text;

public abstract class Driver
{
    //Fields

    //Constructors
    protected Driver(string name, Car car, double fuelConsumptionPerKm)
    {
        this.Name = name;
        this.Car = car;
        this.FuelConsumptionPerKm = fuelConsumptionPerKm;
        
    }

    //Properties
    public string FailureReason { get; set; }
    public bool BeenInBox { get; set; }
    public string Name { get; protected set; }
    public double TotalTime { get; set; }
    public Car Car { get; protected set; }
    public double FuelConsumptionPerKm { get; protected set; }
    public virtual double Speed
    {
        get
        {
            return (this.Car.Hp + this.Car.Tyre.Degradation) / Car.FuelAmount;
        }     
    }

    //Methods
   
}
