using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DriverFactory
{
    private TyreFactory tyreFactory;

    public DriverFactory()
    {
        this.tyreFactory = new TyreFactory();
    }

    public Driver CreateDriver(List<string> driverInfo)
    {
        var type = driverInfo[0];
        var tyre = tyreFactory.CreateTyre(driverInfo.Skip(4).ToList());
        var car = new Car(int.Parse(driverInfo[2]), Double.Parse(driverInfo[3]), tyre);
        var name = driverInfo[1];
        switch (type)
        {
            case "Aggressive":
                return new AggressiveDriver(name, car);
            case "Endurance":
                return new EnduranceDriver(name, car);
        }

        throw new ArgumentException("");
    }
}
