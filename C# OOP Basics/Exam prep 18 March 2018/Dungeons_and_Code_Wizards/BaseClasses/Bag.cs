using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Bag
{
    //FIELDS\\
    private List<Item> items;

    //CONSTRUCTORS\\
    public Bag(int capacity)
    {
        this.items = new List<Item>();
        this.Capacity = capacity;
    }

    //PROPERTIES\\
    public int Capacity { get; private set; }
    public double Load => this.items.Sum(i => i.Weight);
    public IReadOnlyCollection<Item> Items
    {
        get
        {
            return this.items;
        }
    }


    //METHODS\\
    public void AddItem(Item item)
    {
        this.IsOverLoad(item.Weight);

        this.items.Add(item);
    }

    private void IsOverLoad(int weight)
    {
        if(this.Load + weight > this.Capacity)
        {
            throw new InvalidOperationException(ErrorMessages.FullBag);
        }
    }

    public Item GetItem(string name)
    {
        if(items.Count == 0)
        {
            throw new InvalidOperationException(ErrorMessages.EmptyBag);
        }

        var item = items.FirstOrDefault(i => i.GetType().ToString() == name);

        if (item == null)
        {
            throw new ArgumentException(ErrorMessages.NameDoesntExistInBag(name));
        }

        return item;
    }
}
