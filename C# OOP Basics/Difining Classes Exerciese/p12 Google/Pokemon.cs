using System;
using System.Collections.Generic;
using System.Text;

class Pokemon
{
    public string name { get; set; }
    public string type { get; set; }

    public Pokemon(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    public override string ToString()
    {
        if(this.name != null && this.type != null)
        {
            return $"{this.name} {this.type}";
        }

        return "";
    }
}

