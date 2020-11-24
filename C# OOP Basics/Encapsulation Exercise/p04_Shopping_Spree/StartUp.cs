using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    static void Main(string[] args)
    {
        var persons = new List<Person>();

        var personsInfo = Console.ReadLine()
            .Split(";", StringSplitOptions.RemoveEmptyEntries);
        foreach (var personInfo in personsInfo)
        {
            var tokens = personInfo
                .Split("=", StringSplitOptions.RemoveEmptyEntries);

            var personName = tokens[0];
            var personMoney = decimal.Parse(tokens[1]);

            try
            {
                persons.Add(new Person(personName, personMoney));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        var products = new List<Product>();
        var productsInfo = Console.ReadLine()
            .Split(";", StringSplitOptions.RemoveEmptyEntries);
        foreach (var productInfo in productsInfo)
        {
            var tokens = productInfo
                .Split("=", StringSplitOptions.RemoveEmptyEntries);

            var productName = tokens[0];
            var productPrice = decimal.Parse(tokens[1]);

            try
            {
                products.Add(new Product(productName, productPrice));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        var input = Console.ReadLine();
        while (input != "END")
        {
            var tokens = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var personName = tokens[0];
            var productName = tokens[1];

            var currentPerson = persons.FirstOrDefault(p => p.Name == personName);
            var currentProduct = products.FirstOrDefault(p => p.Name == productName);

            currentPerson.AddProduct(currentProduct);

            input = Console.ReadLine();
        }

        foreach (var person in persons)
        {
            var productBag = person
                .ProductsBag.Count > 0 
                ? String.Join(", ", person.ProductsBag.Select(p => p.Name)) 
                : "Nothing bought";
            Console.WriteLine(person.Name + " - " + productBag);
        }

    }
}

