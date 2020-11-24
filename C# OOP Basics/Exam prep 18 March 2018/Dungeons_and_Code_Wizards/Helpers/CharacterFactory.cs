using System;
using System.Collections.Generic;
using System.Text;

public class CharacterFactory
{
    public Character CreateCharacter(string faction, string characterType, string characterName)
    {
        if (faction != "CSharp" && faction != "Java")
        {
            throw new ArgumentException($@"Invalid faction ""{faction}""!");
        }

        switch (characterType)
        {
            case "Warrior":
                return new Warrior(characterName, (Faction)Enum.Parse(typeof(Faction), faction));
            case "Cleric":
                return new Cleric(characterName, (Faction)Enum.Parse(typeof(Faction), faction));
        }

        throw new ArgumentException($@"Invalid character type ""{characterType}""!");
    }
}
