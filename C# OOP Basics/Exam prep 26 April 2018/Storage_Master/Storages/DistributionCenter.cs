using System;
using System.Collections.Generic;
using System.Text;

public class DistributionCenter : Storage
{
    //Fields
    private const int capacity = 2;
    private const int garageSlots = 5;

    //Constructors
    public DistributionCenter(string name)
        : base(name, capacity, garageSlots, new List<Vehicle>() { new Van(), new Van(), new Van() })
    {
    }

    //Properties

    //Methods
}
