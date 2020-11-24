using System;
using System.Collections.Generic;
using System.Text;

public class Warrior : Character, IAttackable
{
    //FIELDS\\
    private const double baseHealth = 100;
    private const double baseArmor = 50;
    private const double baseAbilityPoints = 40;

    //CONSTRUCTORS\\
    public Warrior(string name, Faction faction) 
        : base(name, baseHealth, baseArmor, baseAbilityPoints, new Satchel(), faction)
    {
    }

    //PROPERTIES\\



    //METHODS\\
    public void Attack(Character character)
    {
        this.CheckIsAlive();
        character.CheckIsAlive();

        if (character== this)
        {
            throw new InvalidOperationException(ErrorMessages.SameCharacter);
        }

        if(character.Faction == this.Faction)
        {
            throw new ArgumentException(ErrorMessages.SameFaction(this.Faction));
        }

        character.TakeDamage(this.AbilityPoints);
    }
}
