using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Person
{
    public string name { get; set; }    
    public string birthday { get; set; }
    public List<string> parents { get; set; }
    public List<string> children { get; set; }

    public Person()
    {
        this.parents = new List<string>();
        this.children = new List<string>();
    }

    public Person(string name) : this()
    {
        if (Char.IsDigit(name[0]))
        {
            this.birthday = name;
        }
        else
        {
            this.name = name;
        }
        
    }

    public Person(string name, string birthday) : this(name)
    {
        this.birthday = birthday;
    }

    public override string ToString()
    {
        var result = this.name + " " + this.birthday;
        result += "\nParents:";
        if(this.parents.Count > 0)
        {
            result += "\n" + String.Join("\n", this.parents) + "\n";
        }
        else
        {
            result += "\n";
        }
        result += "Children:";
        if (this.children.Count > 0)
        {
            result += "\n" + String.Join("\n", this.children);
        }
        return result;
    }
}

