namespace TheTankGame.Entities.Parts.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using TheTankGame.Entities.Parts.Contracts;
    using TheTankGame.Entities.Parts.Factories.Contracts;

    public class PartFactory : IPartFactory
    {
        public IPart CreatePart(string partType, string model, double weight, decimal price, int additionalParameter)
        {
            var typeOfPart = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == partType + "Part");

            var instance = Activator.CreateInstance(typeOfPart, new object[] { model, weight, price, additionalParameter });

            return (IPart)instance;
        }
    }
}
