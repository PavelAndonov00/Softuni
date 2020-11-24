using System;
using System.Collections.Generic;
using System.Linq;

public class Potato
{
    static void Main(string[] args)
    {
        long bagMaxCapacity = long.Parse(Console.ReadLine());
        var bag = new Bag(bagMaxCapacity);
        string[] safe = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
       
        for (int i = 0; i < safe.Length; i += 2)
        {           
            string name = safe[i];
            long value = long.Parse(safe[i + 1]);

            if (name.Length == 3)
            {
                bag.AddCash(name, value);
            }
            else if (name.ToLower().EndsWith("gem"))
            {
                bag.AddGem(name, value);
            }
            else if (name.ToLower() == "gold")
            {
                bag.AddGold(name, value);
            }                                
        }

        Console.WriteLine(bag);
    }
}
