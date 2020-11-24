namespace TheTankGame.Entities.Vehicles.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using TheTankGame.Entities.Miscellaneous;
    using TheTankGame.Entities.Miscellaneous.Contracts;
    using TheTankGame.Entities.Vehicles.Factories.Contracts;

    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string vehicleType, string model, double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var typeOfVehicle = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == vehicleType);

            var instance = Activator.CreateInstance(typeOfVehicle, new object[] { model, weight, price, attack, defense, hitPoints, new VehicleAssembler() });

            return (IVehicle)instance;
        }
    }
}
