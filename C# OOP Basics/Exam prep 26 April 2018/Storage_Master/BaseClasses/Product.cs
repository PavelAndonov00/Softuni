using System;
using System.Collections.Generic;
using System.Text;

public abstract class Product
{
    //Fields
    private double price;

    //Constructors
    protected Product(double price, double weight)
    {
        this.Price = price;
        this.Weight = weight;
    }

    //Properties
    public double Price
    {
        get
        {
            return this.price;
        }

        protected set
        {
            if(value < 0)
            {
                throw new InvalidOperationException("Price cannot be negative!");
            }

            this.price = value;
        }
    }
    public double Weight { get; protected set; }

    //Methods
}
