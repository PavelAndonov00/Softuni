using System;
using System.Collections.Generic;
using System.Text;

public class ItemFactory
{
    public Item CreateItem(string itemType)
    {
        switch (itemType)
        {
            case "HealthPotion":
                return new HealthPotion();
            case "PoisonPotion":
                return new PoisonPotion();
            case "ArmorRepairKit":
                return new ArmorRepairKit();
        }

        throw new ArithmeticException($"Invalid item type \"{itemType}\"!");
    }
}
