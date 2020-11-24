using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var people = new List<Person>();
        var input = Console.ReadLine();
        while (input != "End")
        {
            var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var name = tokens[0];
            if(!people.Any(p => p.name == name))
            {
                people.Add(new Person(name));
            }
            var currentPerson = people.Find(p => p.name == name);

            var type = tokens[1];
            if(type == "company")
            {
                var companyName = tokens[2];
                var companyDepartment = tokens[3];
                var salary = decimal.Parse(tokens[4]);

                currentPerson.company = new Company(companyName, companyDepartment, salary);
            }
            else if(type == "pokemon")
            {
                var pokemonName = tokens[2];
                var pokemonType = tokens[3];

                currentPerson.pokemons.Add(new Pokemon(pokemonName, pokemonType));
            }
            else if(type == "parents")
            {
                var parentName = tokens[2];
                var parentBirthday = tokens[3];

                currentPerson.parents.Add(new Parent(parentName, parentBirthday));
            }
            else if(type == "children")
            {
                var childName = tokens[2];
                var childBirthday = tokens[3];

                currentPerson.children.Add(new Child(childName, childBirthday));
            }
            else if(type == "car")
            {
                var carModel = tokens[2];
                var carSpeed = int.Parse(tokens[3]);

                currentPerson.car = new Car(carModel, carSpeed);
            }


            input = Console.ReadLine();
        }

        var personToPrint = Console.ReadLine();

        Console.WriteLine(people.Find(p => p.name == personToPrint).ToString());
    }
}
