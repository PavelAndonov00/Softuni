using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models
{
    public abstract class Animal : IAnimal
    {
        private int happiness;
        private int energy;

        protected Animal(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.Owner = "Centre";
            this.IsAdopt = false;
            this.IsChipped = false;
            this.IsVaccinated = false;
        }

        public string Name { get; protected set; }
        public int Happiness
        {
            get
            {
                return this.happiness;
            }

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Invalid happiness");
                }

                this.happiness = value;
            }
        }
        public int Energy
        {
            get
            {
                return this.energy;
            }

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Invalid energy");
                }

                this.energy = value;
            }
        }
        public int ProcedureTime { get; protected set; }
        public string Owner { get; protected set; }
        public bool IsAdopt { get; protected set; }
        public bool IsChipped { get; protected set; }
        public bool IsVaccinated { get; protected set; }

        public void Adopt()
        {
            this.IsAdopt = true;
        }

        public void ChangeOwner(string owner)
        {
            this.Owner = owner;
        }

        public void DoProcedure(int procedureTime)
        {
            this.ProcedureTime -= procedureTime;
        }

        public override string ToString()
        {
            return $"    Animal type: {this.GetType().Name} - {this.Name} - Happiness: {this.Happiness} - Energy: {this.Energy}";
        }

        public void Chip()
        {
            this.Happiness -= 5;
            this.IsChipped = true;
        }

        public void DoDentalCare()
        {
            this.Happiness += 12;
            this.Energy += 10;
        }

        public void DoFitness()
        {
            this.Happiness -= 3;
            this.Energy += 10;
        }

        public void NailTrim()
        {
            this.Happiness -= 7;
        }

        public void Play()
        {
            this.Energy -= 6;
            this.Happiness += 12;
        }

        public void Vaccinate()
        {
            this.Energy -= 8;
            this.IsVaccinated = true;
        }
    }
}
