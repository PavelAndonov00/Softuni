namespace p03__Barrack_Wars_A_New_Factory.Core.Commands
{
    using _03BarracksFactory.Contracts;
    using _03BarracksFactory.Core.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RetireCommand : Command
    {
        [Injection]
        private IRepository repository;
        [Injection]
        private IUnitFactory unitFactory;

        public RetireCommand(string[] data) : base(data)
        {
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

        public override string Execute()
        {
            string unitType = this.Data[1];
            try
            {
                this.Repository.RemoveUnit(unitType);
                return $"{unitType} retired!";
            }
            catch (ArgumentException ae)
            {
                return ae.Message;
            }

        }
    }
}
