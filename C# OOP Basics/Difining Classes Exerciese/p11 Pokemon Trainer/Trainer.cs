using System;
using System.Collections.Generic;
using System.Text;

class Trainer
{
    public string name { get; set; }
    public int badges { get; set; }
    public List<Pokemon> pokemons { get; set; }

    public Trainer()
    {
        this.pokemons = new List<Pokemon>();
    }

    public Trainer(string name) : this()
    {
        this.name = name;
    }
}

