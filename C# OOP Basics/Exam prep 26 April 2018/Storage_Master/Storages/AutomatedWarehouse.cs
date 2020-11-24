using System;
using System.Collections.Generic;
using System.Text;

public class AutomatedWarehouse : Storage
{
    //Fields
    private const int capacity = 1;
    private const int garageSlots = 2;

    //Constructors
    public AutomatedWarehouse(string name) 
        : base(name, capacity, garageSlots, new List<Vehicle>() { new Truck() })
    {
    }

    //Properties

    //Methods
}
