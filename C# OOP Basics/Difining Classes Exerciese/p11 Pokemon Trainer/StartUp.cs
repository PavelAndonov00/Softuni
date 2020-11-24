using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var trainers = new List<Trainer>();

        var input = Console.ReadLine();
        while (input != "Tournament")
        {
            var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var trainerName = tokens[0];
            var pokemonName = tokens[1];
            var pokemonElement = tokens[2];
            var pokemonHealth = int.Parse(tokens[3]);

            var pokemon = new Pokemon(pokemonName, pokemonElement, pokemonHealth);

            if(trainers.Any(t => t.name == trainerName))
            {
                trainers.Find(t => t.name == trainerName).pokemons.Add(pokemon);
            }
            else
            {
                var trainer = new Trainer(trainerName);
                trainer.pokemons.Add(pokemon);
                trainers.Add(trainer);
            }        

            input = Console.ReadLine();
        }

        input = Console.ReadLine();
        while (input != "End")
        {
            switch (input)
            {
                case "Fire":
                    CheckIfThereIsSuchAPokemon(trainers, input);
                    break;
                case "Water":
                    CheckIfThereIsSuchAPokemon(trainers, input);
                    break;
                case "Electricity":
                    CheckIfThereIsSuchAPokemon(trainers, input);
                    break;
            }

            input = Console.ReadLine();
        }

        trainers
            .OrderByDescending(t => t.badges)
            .Select(t => $"{t.name} {t.badges} {t.pokemons.Count}")
            .ToList()
            .ForEach(Console.WriteLine);
    }

    private static void CheckIfThereIsSuchAPokemon(List<Trainer> trainers, string element)
    {
        foreach (var trainer in trainers)
        {
            if (trainer.pokemons.Any(p => p.element == element))
            {
                trainer.badges++;
            }
            else
            {
                foreach (var pokemon in trainer.pokemons)
                {
                    pokemon.health -= 10;
                }

                trainer.pokemons = trainer.pokemons.Where(p => p.health > 0).ToList();
            }
        }
    }
}

