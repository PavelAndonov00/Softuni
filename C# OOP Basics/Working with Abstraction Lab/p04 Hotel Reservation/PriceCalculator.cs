using System;
using System.Collections.Generic;
using System.Text;

class PriceCalculator
{
    private decimal pricePerDay;
    private int nights;
    private Seasons seasonMultiplier;
    private Discounts discount;

    public PriceCalculator(string input)
    {
        var tokens = input.Split();

        pricePerDay = decimal.Parse(tokens[0]);
        nights = int.Parse(tokens[1]);
        seasonMultiplier = Enum.Parse<Seasons>(tokens[2]);
        discount = Discounts.None;
        if (tokens.Length > 3)
            discount = Enum.Parse<Discounts>(tokens[3]);
    }

    public void CalculatePrice()
    {
        var promoDailyPrice = pricePerDay * ((decimal)100 - (int)discount) / 100;
        var totalPrice = promoDailyPrice * nights * (int)seasonMultiplier;

        Console.WriteLine(totalPrice.ToString("f2"));
    }
}

