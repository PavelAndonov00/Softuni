using System;
using System.Collections.Generic;
using System.Text;

public class Cleric : Character, IHealable
{
    //FIELDS\\
    private const double baseHealth = 50;
    private const double baseArmor = 25;
    private const double baseAbilityPoints = 40;

    //CONSTRUCTORS\\
    public Cleric(string name, Faction faction)
       : base(name, baseHealth, baseArmor, baseAbilityPoints, new Backpack(), faction)
    {
    }

    //PROPERTIES\\
    public override double RestHealMultiplier
    {
        get
        {
            return base.RestHealMultiplier + 0.3;
        }
    }

    //METHODS\\
    public void Heal(Character character)
    {
        this.CheckIsAlive();
        character.CheckIsAlive();

        if (character.Faction != this.Faction)
        {
            throw new InvalidOperationException(ErrorMessages.HealSameFaction);
        }

        character.TakeHeal(this.AbilityPoints);
    }
}
