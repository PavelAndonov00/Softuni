using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

public static class ErrorMessages
{
    //CHARACTER
    public static string AliveChar => "Must be alive to perform this action!";

    public static string SameCharacter => "Cannot attack self!";

    //BAG
    public static string FullBag => "Bag is full!";

    public static string EmptyBag => "Bag is empty!";

    public static string NameDoesntExistInBag(string name)
    {
        return $"No item with name {name} in bag!";
    }

    public static string NullNameOrWhiteSpace => "Name cannot be null or whitespace!";

    
    //FACTION
    public static string SameFaction(Faction faction)
    {
        return $"Friendly fire! Both characters are from {faction.ToString()} faction!";
    }

    public static string HealSameFaction => "Cannot heal enemy character!";
}
