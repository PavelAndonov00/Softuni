using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Vehicle
{
    //Fields
    private Stack<Product> trunk;

    //Constructors
    protected Vehicle(int capacity)
    {
        this.Capacity = capacity;
        this.trunk = new Stack<Product>();
    }

    //Properties
    public int Capacity { get; protected set; }
    
    public IReadOnlyCollection<Product> Trunk
    {
        get
        {
            return this.trunk;
        }
    }

    public bool IsFull
    {
        get
        {
            return this.trunk.Sum(p => p.Weight) >= this.Capacity;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return this.trunk.Count == 0;
        }
    }

    //Methods
    public void LoadProduct(Product product)
    {
        if (this.IsFull)
        {
            throw new InvalidOperationException("Vehicle is full!");
        }

        this.trunk.Push(product);
    }
    
    public Product Unload()
    {
        if (this.IsEmpty)
        {
            throw new InvalidOperationException("No products left in vehicle!");
        }

        return this.trunk.Pop();
    }
}
