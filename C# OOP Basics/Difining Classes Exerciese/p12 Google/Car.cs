using System;
using System.Collections.Generic;
using System.Text;

class Car
{
    public string model { get; set; }
    public int speed { get; set; }

    public Car()
    {

    }

    public Car(string model, int speed) : this()
    {
        this.model = model;
        this.speed = speed;
    }

    public override string ToString()
    {
        var result = "Car:\n";
        if (this.model != null && this.speed != 0)
        {
            result += $"{this.model} {this.speed}\n";
        }

        return result;
    }
}

