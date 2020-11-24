using System;
using System.Collections.Generic;
using System.Text;

class Car
{
    public string model { get; set; }
    public Engine engine { get; set; }
    public string weight { get; set; }
    public string color { get; set; }

    public Car()
    {

    }

    public Car(string model, Engine engine) : this()
    {
        this.model = model;
        this.engine = engine;
    }

    public Car(string model, Engine engine, string weight) : this(model, engine)
    {
        this.weight = weight;
    }

    public Car(string model, Engine engine, string weight, string color) : this(model, engine, weight)
    {
        this.color = color;
    }
}

