using System;
using System.Collections.Generic;
using System.Text;

public class Car
{
    //Fields
    private const double maximumCapacity = 160;
    private double fuelAmount;

    //Constructors
    public Car(int hp, double fuelAmount, Tyre tyre)
    {
        this.Hp = hp;
        this.FuelAmount = fuelAmount;
        this.Tyre = tyre;
    }

    //Properties
    public int Hp { get; protected set; }
    public double FuelAmount
    {
        get
        {
            return this.fuelAmount;
        }

        set
        {
            if (value < 0)
            {
                this.fuelAmount = 0;
                throw new ArgumentException("Out of fuel");
            }

            if (value > maximumCapacity)
            {
                value = maximumCapacity;
            }            

            this.fuelAmount = value;
        }
    }
    public Tyre Tyre { get; set; }

    //Methods
}
