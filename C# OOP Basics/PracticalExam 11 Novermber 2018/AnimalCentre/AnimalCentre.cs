using AnimalCentre.Models;
using AnimalCentre.Models.Animals;
using AnimalCentre.Models.Hotels;
using AnimalCentre.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre
{
    public class AnimalCentre
    {
        private Hotel hotel;
        private List<Procedure> procedures;

        public AnimalCentre()
        {
            this.hotel = new Hotel();
            this.procedures = new List<Procedure>();
            this.InitializeProcedures();
        }

        public string GetAdoptedAnimals()
        {
            return hotel.GetAdoptedAnimals();
        }

        private void InitializeProcedures()
        {
            this.procedures.Add(new Chip());
            this.procedures.Add(new DentalCare());
            this.procedures.Add(new Fitness());
            this.procedures.Add(new NailTrim());
            this.procedures.Add(new Play());
            this.procedures.Add(new Vaccinate());
        }

        public string RegisterAnimal(string type, string name, int energy, int happiness, int procedureTime)
        {
            switch (type)
            {
                case "Cat":
                    hotel.Accommodate(new Cat(name, energy, happiness, procedureTime));
                    break;
                case "Dog":
                    hotel.Accommodate(new Dog(name, energy, happiness, procedureTime));
                    break;
                case "Pig":
                    hotel.Accommodate(new Pig(name, energy, happiness, procedureTime));
                    break;
                case "Lion":
                    hotel.Accommodate(new Lion(name, energy, happiness, procedureTime));
                    break;

            }

            return $"Animal {name} registered successfully";
        }

        public string Chip(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var chipProcedure = procedures.FirstOrDefault(p => p is Chip);
            chipProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} had chip procedure";
        }

        public string Vaccinate(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var vaccinateProcedure = procedures.FirstOrDefault(p => p is Vaccinate);
            vaccinateProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} had vaccination procedure";
        }

        public string Fitness(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var fitnessProcedure = procedures.FirstOrDefault(p => p is Fitness);
            fitnessProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} had fitness procedure";
        }

        public string Play(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var playProcedure = procedures.FirstOrDefault(p => p is Play);
            playProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} was playing for {procedureTime} hours";
        }

        public string DentalCare(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var dentalProcedure = procedures.FirstOrDefault(p => p is DentalCare);
            dentalProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} had dental care procedure";
        }

        public string NailTrim(string name, int procedureTime)
        {
            if (!hotel.ContainsName(name))
            {
                throw new ArgumentException($"Animal {name} does not exist");
            }

            var currentAnimal = hotel.GetAnimal(name);
            var nailProcedure = procedures.FirstOrDefault(p => p is NailTrim);
            nailProcedure.DoService(currentAnimal, procedureTime);

            return $"{name} had nail trim procedure";
        }

        public string Adopt(string animalName, string owner)
        {
            var currentAnimal = hotel.GetAnimal(animalName);
            hotel.Adopt(animalName, owner);

            if (currentAnimal.IsChipped)
            {
                return $"{owner} adopted animal with chip";
            }

            return $"{owner} adopted animal without chip";
        }

        public string History(string type)
        {
            var currentProcedure = procedures.FirstOrDefault(p => p.GetType().Name == type);

            return currentProcedure.AnimalHistory();
        }


    }
}
