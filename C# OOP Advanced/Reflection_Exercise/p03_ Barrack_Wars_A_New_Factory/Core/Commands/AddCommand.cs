namespace p03__Barrack_Wars_A_New_Factory.Core.Commands
{
    using _03BarracksFactory.Contracts;
    using _03BarracksFactory.Core.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AddCommand : Command
    {
        [Injection]
        private IRepository repository;
        [Injection]
        private IUnitFactory unitFactory;

        public AddCommand(string[] data) : base(data)
        {
        }

        public override string Execute()
        {
            string unitType = this.Data[1];
            IUnit unitToAdd = this.UnitFactory.CreateUnit(unitType);
            this.Repository.AddUnit(unitToAdd);
            string output = unitType + " added!";
            return output;
        }

        public IRepository Repository
        {
            get => this.repository;
            set => this.repository = value;
        }

        public IUnitFactory UnitFactory
        {
            get => this.unitFactory;
            set => this.unitFactory = value;
        }
    }
}
