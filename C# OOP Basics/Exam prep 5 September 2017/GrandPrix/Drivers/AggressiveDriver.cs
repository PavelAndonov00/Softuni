using System;
using System.Collections.Generic;
using System.Text;

public class AggressiveDriver : Driver
{
    //Fields
    private const double fuelConsumptionPerKm = 2.7;

    //Constructors
    public AggressiveDriver(string name, Car car) : base(name, car, fuelConsumptionPerKm)
    {
    }

    //Properties
    public override double Speed => base.Speed * 1.3;

    //Methods

}
