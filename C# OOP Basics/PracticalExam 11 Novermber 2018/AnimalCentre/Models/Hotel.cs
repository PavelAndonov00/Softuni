using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre.Models.Hotels
{
    public class Hotel : IHotel
    {
        private const int capacity = 10;
        private Dictionary<string, Animal> animals;
        private Dictionary<string, List<Animal>> adoptedAnimals;

        public Hotel()
        {
            this.animals = new Dictionary<string, Animal>();
            this.adoptedAnimals = new Dictionary<string, List<Animal>>();
            this.Capacity = capacity;
        }

        public int Capacity { get; private set; }
        public IReadOnlyDictionary<string, Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        public IReadOnlyDictionary<string, List<Animal>> AdoptedAnimals
        {
            get
            {
                return this.adoptedAnimals;
            }
        }

        public void Accommodate(IAnimal animal)
        {
            var castedAnimal = (Animal)animal;
            if(this.Capacity <= 0)
            {
                throw new InvalidOperationException("Not enough capacity");
            }

            var animalName = castedAnimal.Name;
            if (animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"Animal {animalName} already exist");
            }

            this.animals[animalName] = castedAnimal;
            this.Capacity--;
        }

        public void Adopt(string animalName, string owner)
        {
            if (!animals.ContainsKey(animalName))
            {
                throw new ArgumentException($"Animal {animalName} does not exist");
            }

            var currentAnimal = this.animals[animalName];
            currentAnimal.ChangeOwner(owner);
            currentAnimal.Adopt();

            if (!this.adoptedAnimals.ContainsKey(owner))
            {
                this.adoptedAnimals[owner] = new List<Animal>();
            }
            this.adoptedAnimals[owner].Add(currentAnimal);
            this.animals.Remove(animalName);
        }

        public bool ContainsName(string name)
        {
            return this.animals.ContainsKey(name);
        }

        public Animal GetAnimal(string name)
        {
            return this.animals[name];
        }

        public string GetAdoptedAnimals()
        {
            var sb = new StringBuilder();

            foreach (var adoptedAnimal in adoptedAnimals.OrderBy(a => a.Key))
            {
                sb.AppendLine($"--Owner: {adoptedAnimal.Key}");
                sb.AppendLine($"   - Adopted animals: {String.Join(" ", adoptedAnimal.Value.Select(a => a.Name))}");
            }

            return sb.ToString().Trim();
        }
    }
}
