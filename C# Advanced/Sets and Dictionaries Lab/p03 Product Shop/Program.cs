using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_Product_Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var result = new Dictionary<string, Dictionary<string, double>>();
            while (input != "Revision")
            {
                var tokens = input
                    .Split(", ")
                    .ToArray();

                var supermarket = tokens[0];
                var product = tokens[1];
                var price = double.Parse(tokens[2]);

                if (!result.ContainsKey(supermarket))
                {
                    result[supermarket] = new Dictionary<string, double>();
                }

                result[supermarket][product] = price;

                input = Console.ReadLine();
            }

            foreach (var supermarket in result.Keys.OrderBy(e => e))
            {
                Console.WriteLine(supermarket + "->");
                foreach (var product in result[supermarket].Keys)
                {
                    Console.WriteLine("Product: " + product + ", Price: " + result[supermarket][product]);
                }
            }
        }
    }
}
