using System;
using System.Collections.Generic;
using System.Text;

public class EnduranceDriver : Driver 
{
    //Fields
    private const double fuelConsumptionPerKm = 1.5;

    //Constructors
    public EnduranceDriver(string name, Car car) : base(name, car, fuelConsumptionPerKm)
    {
    }

    //Properties

    //Methods
}
