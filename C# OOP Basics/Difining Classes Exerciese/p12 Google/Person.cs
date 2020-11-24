using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Person
{
    public string name { get; set; }
    public Car car { get; set; }
    public Company company { get; set; }
    public List<Pokemon> pokemons { get; set; }
    public List<Parent> parents { get; set; }
    public List<Child> children { get; set; }

    public Person()
    {
        this.car = new Car();
        this.company = new Company();
        this.pokemons = new List<Pokemon>();
        this.parents = new List<Parent>();
        this.children = new List<Child>();
    }

    public Person(string name) : this()
    {
        this.name = name;
    }

    public override string ToString()
    {
        var result = this.name + "\n";
        result += this.company.ToString();
        result += this.car.ToString();
        result += "Pokemon:";
        if(this.pokemons.Count > 0)
        {
            result += "\n" + String.Join("\n", this.pokemons.Select(p => p.ToString()));
        }       
        result += "\nParents:";
        if(this.parents.Count > 0)
        {
            result += "\n" + String.Join("\n", this.parents.Select(p => p.ToString()));
        }
        result += "\nChildren:";
        if(this.children.Count > 0)
        {
            result += "\n" + String.Join("\n", this.children.Select(c => c.ToString()));
        }
        return result;
    }
}

