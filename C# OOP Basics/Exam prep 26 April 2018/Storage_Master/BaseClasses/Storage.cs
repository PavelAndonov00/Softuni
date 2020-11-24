using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Storage 
{
    //Fields
    private List<Vehicle> garage;
    private List<Product> products;

    //Constructors
    protected Storage(string name, int capacity, int garageSlots, IEnumerable<Vehicle> vehicles)
    {
        this.Name = name;
        this.Capacity = capacity;
        this.GarageSlots = garageSlots;
        this.garage = new List<Vehicle>();
        this.InitializeGarage(garageSlots, (List<Vehicle>)vehicles);
        this.products = new List<Product>();
    }

    protected void InitializeGarage(int garageSlots, List<Vehicle> vehicles)
    {
        for (int i = 0; i < garageSlots; i++)
        {
            if(i <= vehicles.Count - 1)
            {
                this.garage.Add(vehicles[i]);
            }
            else
            {
                this.garage.Add(null);
            }
        }
    }

    //Properties
    public string Name { get; protected set; }
    public int Capacity { get; protected set; }
    public int GarageSlots { get; protected set; }
    
    public bool IsFull
    {
        get
        {
            return this.products.Sum(p => p.Weight) >= this.Capacity;
        }
    }

    public IReadOnlyCollection<Vehicle> Garage
    {
        get
        {
            return this.garage;
        }
    }

    public IReadOnlyCollection<Product> Products
    {
        get
        {
            return this.products;
        }
    }

    //Methods
    public Vehicle GetVehicle(int garageSlot)
    {
        if(garageSlot >= this.GarageSlots)
        {
            throw new InvalidOperationException("Invalid garage slot!");
        }

        var currentVihicle = this.garage[garageSlot];
        if (currentVihicle == null)
        {
            throw new InvalidOperationException("No vehicle in this garage slot!");
        }

        return currentVihicle;
    }

    public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
    {
        var currentVehicle = this.GetVehicle(garageSlot);

        if (!deliveryLocation.IsThereFreeSlot())
        {
            throw new InvalidOperationException("No room in garage!");
        }

        this.garage[garageSlot] = null;
        deliveryLocation.AddVihicle(currentVehicle);

        return deliveryLocation.GetIndexOf(currentVehicle);
    }

    protected bool IsThereFreeSlot()
    {
        return this.garage.Any(v => v == null);
    }

    public int UnloadVehicle(int garageSlot)
    {
        if (this.IsFull)
        {
            throw new InvalidOperationException("Storage is full!");
        }

        var currentVihicle = this.GetVehicle(garageSlot);

        var products = (Stack<Product>)currentVihicle.Trunk;
        var unloadedProducts = 0;
        while (products.Count > 0 && !this.IsFull)
        {
            this.products.Add(currentVihicle.Unload());
            unloadedProducts++;
        }

        return unloadedProducts;
    }

    protected void AddVihicle(Vehicle vihicle)
    {
        for (int i = 0; i < garage.Count; i++)
        {
            if(garage[i] == null)
            {
                garage[i] = vihicle;
                break;
            }
        } 
    }
    
    protected int GetIndexOf(Vehicle vehicle)
    {
        return this.garage.IndexOf(vehicle);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{this.Name}:");
        sb.AppendLine($"Storage worth: ${this.products.Sum(p => p.Price):F2}");

        return sb.ToString().Trim();

    }
}
