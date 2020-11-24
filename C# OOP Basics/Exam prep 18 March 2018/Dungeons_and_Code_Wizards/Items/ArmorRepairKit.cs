using System;
using System.Collections.Generic;
using System.Text;

public class ArmorRepairKit : Item
{
    //FIELDS\\
    private const int armourWeight = 10;

    //CONSTRUCTORS\\
    public ArmorRepairKit()
        :base(armourWeight)
    {
    }

    //PROPERTIES\\

    //METHODS\\
    public override void AffectCharacter(Character character)
    {
        base.AffectCharacter(character);

        character.UseArmourRepairKit();
    }
}
