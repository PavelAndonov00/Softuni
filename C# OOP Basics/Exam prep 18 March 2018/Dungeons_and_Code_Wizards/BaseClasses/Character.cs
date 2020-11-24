using System;
using System.Collections.Generic;
using System.Text;

public abstract class Character
{
    //FIELDS\\
    private double health;
    private string name;
    private double armor;

    //CONSTRUCTORS\\
    protected Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
    {
        this.Name = name;
        this.BaseHealth = health;
        this.Health = health;
        this.BaseArmor = armor;
        this.Armor = armor;
        this.AbilityPoints = abilityPoints;
        this.Bag = bag;
        this.Faction = faction;
        this.IsAlive = true;
        this.RestHealMultiplier = 0.2;
    }

    //PROPERTIES\\
    public string Name
    {
        get
        {
            return this.name;
        }
        private set
        {
            if (String.IsNullOrWhiteSpace(value) || String.IsNullOrEmpty(value))
            {
                throw new ArgumentException(ErrorMessages.NullNameOrWhiteSpace);
            }

            this.name = value;
        }
    }
    public double BaseHealth { get; protected set; }
    public double Health
    {
        get
        {
            return this.health;
        }
        protected set
        {
            if(value < 0)
            {
                this.Health = 0;
                this.IsAlive = false;
            }

            this.health = Math.Min(Math.Max(value, 0), this.BaseHealth);
        }
    }
    public double BaseArmor { get; protected set; }
    public double Armor
    {
        get
        {
            return this.armor;
        }
        protected set
        {           
            this.armor = Math.Min(Math.Max(value, 0), this.BaseArmor);
        }
    }
    public double AbilityPoints { get; protected set; }
    public Bag Bag { get; protected set; }
    public Faction Faction { get; protected set; }
    public bool IsAlive { get; protected set; }
    public virtual double RestHealMultiplier { get; protected set; }


    //METHODS\\
    public void CheckIsAlive()
    {
        if (!this.IsAlive)
        {
            throw new InvalidOperationException(ErrorMessages.AliveChar);
        }
    }

    public void UseHealthPotion(double healthPoints)
    {
        this.Health += healthPoints;
    }

    public void UsePoisonPotion(double healthPoints)
    {
        this.Health -= healthPoints;
    }
    
    public void UseArmourRepairKit()
    {
        this.Armor = this.BaseArmor;
    }

    public void TakeDamage(double hitPoints)
    {
        this.CheckIsAlive();
        
        if(this.Armor < hitPoints)
        {
            hitPoints -= this.Armor;
            this.Armor = 0;

            this.Health -= hitPoints;
        }

        this.Armor -= hitPoints;
    }

    public void TakeHeal(double hitPoints)
    {
        this.CheckIsAlive();

        this.Health += hitPoints;
    }

    public void Rest()
    {
        this.CheckIsAlive();

        this.Health += this.BaseHealth * this.RestHealMultiplier;
    }

    public void UseItem(Item item)
    {
        item.AffectCharacter(this);
    }

    public void UseItemOn(Item item, Character character)
    {
        this.CheckIsAlive();
        character.CheckIsAlive();

        character.UseItem(item);
    }

    public void GiveCharacterItem(Item item, Character character)
    {
        this.CheckIsAlive();
        character.CheckIsAlive();

        character.Bag.AddItem(item);
    }

    public void ReceiveItem(Item item)
    {
        this.CheckIsAlive();

        this.Bag.AddItem(item);
    }

    public override string ToString()
    {
        var status = this.IsAlive == true ? "Alive" : "Dead";
        return $"{this.Name} - HP: {this.Health}/{this.BaseHealth}, AP: {this.Armor}/{this.BaseArmor}, Status: {status}";
    }
}
