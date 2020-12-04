namespace _02.Data
{
    using _02.Data.Interfaces;
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> _bag;

        public Data()
        {
            this._bag = new OrderedBag<IEntity>((x, y) => y.Id - x.Id);
        }

        public Data(Data copy)
        {
            this._bag = copy._bag;
        }

        public int Size => this._bag.Count;

        public void Add(IEntity entity)
        {
            this._bag.Add(entity);

            var parent = this.GetById((int)entity.ParentId);
            if(parent != null)
            {
                parent.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {
            return new Data((Data)this.MemberwiseClone());
        }

        public IEntity DequeueMostRecent()
        {
            this.ThrowIfTryingGetTopEmptyCollection();
            return this._bag.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            var result = new List<IEntity>();
            foreach (var item in this._bag)
            {
                result.Add(item);
            }
            return result;
        }

        public List<IEntity> GetAllByType(string type)
        {
            var result = new List<IEntity>();
            foreach (var item in this._bag)
            {
                if (item.GetType().Name == type)
                {
                    result.Add(item);
                }
            }
            if (result.Count == 0)
            {
                this.ThrowInvalidOperationException("Invalid type: " + type);
            }

            return result;
        }

        public IEntity GetById(int id)
        {
            if (id < 0 || id > this.Size)
            {
                return null;
            }

            return this._bag[this.Size - 1 - id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            var parent = this.GetById(parentId);
            return parent.Children;
        }

        public IEntity PeekMostRecent()
        {
            this.ThrowIfTryingGetTopEmptyCollection();
            return this._bag.GetFirst();
        }

        private void ThrowIfTryingGetTopEmptyCollection()
        {
            if (this.Size == 0)
            {
                this.ThrowInvalidOperationException("Operation on empty Data");
            }
        }

        private void ThrowInvalidOperationException(string message)
        {
            throw new InvalidOperationException(message);
        }
    }
}
