using System;
using System.Collections.Generic;
using System.Text;

public class Warehouse : Storage
{
    //Fields
    private const int capacity = 10;
    private const int garageSlots = 10;

    //Constructors
    public Warehouse(string name)
        : base(name, capacity, garageSlots, new List<Vehicle>() { new Semi(), new Semi(), new Semi() })
    {
    }

    //Properties

    //Methods
}
