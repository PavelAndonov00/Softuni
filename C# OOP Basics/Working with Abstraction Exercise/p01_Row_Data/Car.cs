using System;
using System.Collections.Generic;
using System.Text;

class Car
{
    public string Model { get; set; }
    public Engine CarEngine { get; set; }
    public Cargo CarCargo { get; set; }
    public List<Tire> Tires { get; set; }

    public Car(string model, Engine engine, Cargo cargo, List<Tire> tires)
    {
        Model = model;
        CarEngine = engine;
        CarCargo = cargo;
        Tires = tires;
    }    
}
