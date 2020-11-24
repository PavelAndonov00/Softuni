using System;
using System.Collections.Generic;
using System.Text;

public class RandomList:List<string>
{
    public string RandomString()
    {
        var random = new Random();
        var randomIndex = random.Next(0, this.Count - 1);
        return this[randomIndex];
    }
}

