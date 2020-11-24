using System;
using System.Collections.Generic;
using System.Text;
using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Procedures
{
    public class Chip : Procedure
    {
        public override void DoService(IAnimal animal, int procedureTime)
        {
            base.DoService(animal,procedureTime);
            var castedAnimal = (Animal)animal;
            if (castedAnimal.IsChipped)
            {
                throw new ArgumentException($"{castedAnimal.Name} is already chipped");
            }

            castedAnimal.Chip();
        }
    }
}
