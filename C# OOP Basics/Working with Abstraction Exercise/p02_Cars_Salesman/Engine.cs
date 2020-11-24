using System;
using System.Collections.Generic;
using System.Text;

class Engine
{
    public string model;
    public int power;
    public int displacement;
    public string efficiency;

    public Engine()
    {
        this.displacement = -1;
        this.efficiency = "n/a";
    }

    public Engine(string model, int power) : this()
    {
        this.model = model;
        this.power = power;
    }

    public Engine(string model, int power, int displacement) : this(model, power)
    {
        this.displacement = displacement;
    }

    public Engine(string model, int power, string efficiency) : this(model, power)
    {
        this.efficiency = efficiency;
    }

    public Engine(string model, int power, int displacement, string efficiency) : this(model, power)
    {
        this.displacement = displacement;
        this.efficiency = efficiency;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"  {this.model}:\n");
        sb.Append($"    Power: {this.power}\n");
        sb.AppendFormat("    Displacement: {0}\n", this.displacement == -1 ? "n/a" : this.displacement.ToString());
        sb.AppendFormat($"    Efficiency: {this.efficiency}\n");

        return sb.ToString();
    }
}
