using System;
using System.Collections.Generic;
using System.Text;

public class HealthPotion : Item
{
    //FIELDS\\
    private const int healthPotionWeight = 5;
    private const int healingPoints = 20;

    //CONSTRUCTORS\\
    public HealthPotion() : base(healthPotionWeight)
    {
    }

    //PROPERTIES\\

    //METHODS\\
    public override void AffectCharacter(Character character)
    {
        base.AffectCharacter(character);

        character.UseHealthPotion(healingPoints);
    }


}
