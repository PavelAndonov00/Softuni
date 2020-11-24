using System;
using System.Collections.Generic;
using System.Text;

public class Person
{
    private string name;
    private decimal money;
    private List<Product> productsBag;

    public Person(string name, decimal money)
    {
        this.Name = name;
        this.Money = money;
        this.productsBag = new List<Product>();
    }

    public string Name
    {
        get { return name; }
        private set
        {
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }

            name = value;
        }
    }
   
    public decimal Money
    {
        get { return money; }
        private set
        {
            if(value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }

            money = value;
        }
    }

    public IReadOnlyList<Product> ProductsBag
    {
        get
        {
            return this.productsBag;
        }        
    }

    public void AddProduct(Product product)
    {
        if(Money >= product.Cost)
        {
            Money -= product.Cost;
            this.productsBag.Add(product);

            Console.WriteLine(Name + " bought " + product.Name);
        }
        else
        {
            Console.WriteLine(Name + " can't afford " + product.Name);
        }
    }   

}

  

