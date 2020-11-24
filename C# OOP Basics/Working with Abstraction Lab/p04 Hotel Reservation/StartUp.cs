using System;

class StartUp
{
    static void Main(string[] args)
    {
        var input = Console.ReadLine();
        var priceCalculator = new PriceCalculator(input);
        priceCalculator.CalculatePrice();
    }
}

