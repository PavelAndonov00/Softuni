using _03BarracksFactory.Contracts;
using _03BarracksFactory.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace p03__Barrack_Wars_A_New_Factory.Core.Commands
{
    public class ReportCommand : Command
    {
        [Injection]
        private IRepository repository;
        [Injection]
        private IUnitFactory unitFactory;

        public ReportCommand(string[] data)
            : base(data)
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
            string output = this.Repository.Statistics;
            return output;
        }
    }
}
