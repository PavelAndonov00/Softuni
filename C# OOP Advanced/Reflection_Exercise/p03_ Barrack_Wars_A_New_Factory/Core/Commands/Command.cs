namespace _03BarracksFactory.Core.Commands
{
    using _03BarracksFactory.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Command : IExecutable
    {
        private string[] data;

        protected Command(string[] data)
        {
            this.Data = data;
        }

        protected string[] Data
        {
            get => this.data;
            set => this.data = value;           
        }
       
        public abstract string Execute();
    }
}
