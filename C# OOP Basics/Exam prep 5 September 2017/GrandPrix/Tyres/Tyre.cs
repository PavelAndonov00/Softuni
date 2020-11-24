using System;
using System.Collections.Generic;
using System.Text;

public abstract class Tyre
{
    //Fields
    private double degradation;
    private const double baseDegradation = 100;

    //Constructors
    protected Tyre(string name, double hardness)
    {
        this.Name = name;
        this.Hardness = hardness;
        this.Degradation = baseDegradation;
    }   

    //Properties
    public string Name { get; protected set; }
    public double Hardness { get; protected set; }

    public virtual double Degradation
    {
        get { return degradation; }
        protected set
        {
            if(value < 0)
            {
                this.degradation = 0;
                throw new ArgumentException("Blown Tyre");
            }

            degradation = value;
        }
    }

    //Methods
    public virtual void Degradate()
    {
        this.Degradation -= this.Hardness;
    }
}
