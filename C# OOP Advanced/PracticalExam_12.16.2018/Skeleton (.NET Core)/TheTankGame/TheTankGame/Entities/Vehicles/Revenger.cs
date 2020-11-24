namespace TheTankGame.Entities.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TheTankGame.Entities.Miscellaneous.Contracts;
    using TheTankGame.Entities.Vehicles.Contracts;

    public class Revenger : BaseVehicle
    {
        public Revenger(string model, double weight, decimal price, int attack, int defense, int hitPoints, IAssembler assembler) : base(model, weight, price, attack, defense, hitPoints, assembler)
        {
        }
    }
}