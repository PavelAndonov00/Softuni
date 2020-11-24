using System;
using System.Collections.Generic;
using System.Text;

public class ProductFactory
{
    public Product CreateProduct(string type, double price)
    {
        switch (type)
        {
            case "Gpu":
                return new Gpu(price);
            case "HardDrive":
                return new HardDrive(price);
            case "SolidStateDrive":
                return new SolidStateDrive(price);
            case "Ram":
                return new Ram(price);
        }

        throw new InvalidOperationException("Invalid product type!");
    }
}
