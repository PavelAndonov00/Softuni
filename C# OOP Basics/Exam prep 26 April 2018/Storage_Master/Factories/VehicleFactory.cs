using System;
using System.Collections.Generic;
using System.Text;

public class VehicleFactory
{
    public Vehicle CreateVehicle(string type)
    {
        switch (type)
        {
            case "Truck":
                return new Truck();
            case "Semi":
                return new Semi();
            case "Van":
                return new Van();
        }

        throw new InvalidOperationException("Invalid vehicle type!");
    }
}
