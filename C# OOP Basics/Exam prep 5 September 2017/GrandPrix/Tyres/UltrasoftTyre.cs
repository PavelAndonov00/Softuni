using System;
using System.Collections.Generic;
using System.Text;

public class UltrasoftTyre : Tyre
{
    //Fields
    private const string baseName = "Ultrasoft";

    //Constructors
    public UltrasoftTyre(double hardness, double grip) : base(baseName, hardness)
    {
        this.Grip = grip;
    }

    //Properties
    public double Grip { get; protected set; }
    public override double Degradation
    {
        get => base.Degradation;
        protected set
        {
            if (value < 30)
            {
                throw new ArgumentException("Blown Tyre");
            }

            base.Degradation = value;
        }
    }

    //Methods
    public override void Degradate()
    {
        base.Degradate();

        this.Degradation -= this.Grip;
    }
}
