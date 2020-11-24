using System;
using System.Collections.Generic;
using System.Text;

class Car
{
    private string model;
    private int enginePower;
    private string cargoType;
    private List<double> tirePressures;

    public string Model
    {
        get
        {
            return this.model;
        }
        set
        {
            this.model = value;
        }
    }

    public int EnginePower
    {
        get
        {
            return this.enginePower;
        }
        set
        {
            this.enginePower = value;
        }
    }

    public string CargoType
    {
        get
        {
            return this.cargoType;
        }
        set
        {
            this.cargoType = value;
        }
    }

    public List<double> TirePressures
    {
        get
        {
            return this.tirePressures;
        }
        set
        {
            this.tirePressures = value;
        }
    }

    public Car()
    {
        this.tirePressures = new List<double>();
    }

    public Car(string model, int enginePower, string cargoType, List<double> tirePressures) : this()
    {
        this.Model = model;
        this.EnginePower = enginePower;
        this.CargoType = cargoType;
        this.TirePressures = tirePressures;
    }

}

