using System;
using System.Collections.Generic;
using System.Text;

class Engine
{
    public string model { get; set; }
    public int power { get; set; }
    public string displacement { get; set; }
    public string efficiency { get; set; }

    public Engine()
    {

    }

    public Engine(string model, string power)
    {
        this.model = model;
        this.power = int.Parse(power);
    }

    public Engine(string model, string power, string displacement) 
        : this(model, power)
    {
        this.displacement = displacement;
    }

    public Engine(string model, string power, string displacement, string efficiency) 
        :this(model, power, displacement)
    {
        this.efficiency = efficiency;
    }
}

