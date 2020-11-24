using System;
using System.Collections.Generic;
using System.Text;

public abstract class Item
{
    //FIELDS\\

    //CONSTRUCTORS\\
    public Item()
    {

    }

    public Item(int weight)
    {
        this.Weight = weight;
    }

    //PROPERTIES\\
    public int Weight { get; private set; }

    //METHODS\\
    public virtual void AffectCharacter(Character character)
    {
        character.CheckIsAlive();
    }
}
