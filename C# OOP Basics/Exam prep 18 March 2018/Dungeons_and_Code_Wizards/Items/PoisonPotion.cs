using System;
using System.Collections.Generic;
using System.Text;

public class PoisonPotion : Item
{
    
        //FIELDS\\
        private const int healthPotionWeight = 5;
        private const double healingPoints = 20;

        //CONSTRUCTORS\\
        public PoisonPotion() : base(healthPotionWeight)
        {
        }

    //PROPERTIES\\

    //METHODS\\
    public override void AffectCharacter(Character character)
    {
        base.AffectCharacter(character);

        character.UsePoisonPotion(healingPoints);
    }
}
