using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre.Models.Procedures
{
    public abstract class Procedure
    {
        private List<Animal> procedureHistory;

        public Procedure()
        {
            this.procedureHistory = new List<Animal>();
        }

        public List<Animal> ProcedureHistory
        {
            get
            {
                return this.procedureHistory;
            }
        }

        public string History()
        {
            var builder = new StringBuilder();
            builder.AppendLine(this.GetType().Name);
            foreach (var animal in ProcedureHistory)
            {
                builder.AppendLine($"    - {animal.Name} - Happiness: {animal.Happiness} - Energy: {animal.Energy}");
            }

            return builder.ToString().Trim();
        }

        public string AnimalHistory()
        {
            var builder = new StringBuilder();
            builder.AppendLine(this.GetType().Name);
            foreach (var animal in ProcedureHistory)
            {
                builder.AppendLine($"    Animal type: {animal.GetType().Name} - {animal.Name} - Happiness: {animal.Happiness} - Energy: {animal.Energy}");
            }

            return builder.ToString().Trim();
        }

        public virtual void DoService(IAnimal animal, int procedureTime)
        {
            var castedAnimal = (Animal)animal;
            if (procedureTime > castedAnimal.ProcedureTime)
            {
                throw new ArgumentException("Animal doesn't have enough procedure time");
            }

            castedAnimal.DoProcedure(procedureTime);
            ProcedureHistory.Add(castedAnimal);
        }
    }
}
