using System;
using System.Collections.Generic;
using System.Text;

public class StorageFactory
{
    public Storage CreateStorage(string type, string name)
    {
        switch (type)
        {
            case "AutomatedWarehouse":
                return new AutomatedWarehouse(name);
            case "Warehouse":
                return new Warehouse(name);
            case "DistributionCenter":
                return new DistributionCenter(name);
        }

        throw new InvalidOperationException("Invalid storage type!");
    }
}
