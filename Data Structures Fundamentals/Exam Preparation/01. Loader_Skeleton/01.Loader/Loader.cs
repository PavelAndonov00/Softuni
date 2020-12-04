namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Loader : IBuffer
    {
        private Dictionary<int, List<IEntity>> _entities;

        public Loader()
        {
            this._entities = new Dictionary<int, List<IEntity>>();
        }

        // O(1)
        public int EntitiesCount => this.GetCount();

        private int GetCount()
        {
            var count = 0;
            foreach (var v in this._entities.Values)
            {
                count += v.Count;
            }

            return count;
        }

        // O(amortised)
        public void Add(IEntity entity)
        {
            if (!this._entities.ContainsKey(entity.Id))
            {
                this._entities[entity.Id] = new List<IEntity>();

            }

            this._entities[entity.Id].Add(entity);
        }

        // O(1)
        public void Clear()
        {
            this._entities.Clear();
        }

        // O(n) 
        public bool Contains(IEntity entity)
        {
            if (this._entities.ContainsKey(entity.Id))
            {
                var list = this._entities[entity.Id];
                if (list.Count > 1)
                {
                    foreach (var v in list)
                    {
                        if (v.Id == entity.Id && v.ParentId == entity.ParentId && v.Status == entity.Status)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    return true;
                }

            }

            return false;
        }

        // O(n)
        public IEntity Extract(int id)
        {
            if (this._entities.ContainsKey(id))
            {
                var current = this._entities[id][0];
                this._entities.Remove(id);
                return current;
            }

            return null;
        }

        // O(1)
        public IEntity Find(IEntity entity)
        {
            if (this.Contains(entity))
            {
                var list = this._entities[entity.Id];
                if (list.Count > 1)
                {
                    foreach (var v in list)
                    {
                        if (v.Equals(entity))
                        {
                            return v;
                        }
                    }
                }
                else
                {
                    return list[0];
                }
            }

            return null;
        }

        // O(n)
        public List<IEntity> GetAll()
        {
            var result = new List<IEntity>();
            foreach (var kvp in this._entities.Values)
            {
                foreach (var item in kvp)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        // O(n)
        public void RemoveSold()
        {
            foreach (var kvp in this._entities)
            {
                var list = kvp.Value;
                if (list.Count > 1)
                {
                    var removeList = new List<IEntity>();
                    foreach (var v in list)
                    {
                        if (v.Status == BaseEntityStatus.Sold)
                        {
                            removeList.Add(v);
                        }
                    }

                    foreach (var ri in removeList)
                    {
                        list.Remove(ri);
                    }
                }
                else
                {
                    if (list[0].Status == BaseEntityStatus.Sold)
                    {
                        list.Clear();
                    }
                }

            }
        }

        // O(n)
        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            var oldExists = this.Contains(oldEntity);
            if (oldExists)
            {
                this._entities.Remove(oldEntity.Id);
                this.Add(newEntity);
                return;
            }

            throw new InvalidOperationException("Entity not found");
        }

        // O(n)
        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>();
            foreach (var value in this._entities)
            {
                var list = value.Value;
                if (list.Count > 1)
                {
                    foreach (var v in list)
                    {
                        if (v.Status >= lowerBound && v.Status <= upperBound)
                        {
                            result.Add(v);
                        }
                    }
                }
                else
                {
                    if (list[0].Status >= lowerBound && list[0].Status <= upperBound)
                    {
                        result.Add(list[0]);
                    }
                }
            }

            return result;
        }

        // O(n)
        public void Swap(IEntity first, IEntity second)
        {
            var firstExists = this._entities.ContainsKey(first.Id);
            var secondExists = this._entities.ContainsKey(second.Id);

            if (!firstExists || !secondExists)
            {
                throw new InvalidOperationException("Entity not found");
            }

            var firstEn = this.Find(first);
            var secEn = this.Find(second);
            if (this._entities[first.Id].Count == 1)
            {
                this._entities[first.Id][0] = secEn;
            }

            this._entities[second.Id][0] = firstEn;
        }

        // O(n)
        public IEntity[] ToArray()
        {
            var list = new List<IEntity>();
            foreach (var value in this._entities.Values)
            {
                foreach (var item in value)
                {
                    list.Add(item);
                }
            }

            return list.ToArray();
        }

        // O(n)
        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            foreach (var kvp in this._entities.Values)
            {
                foreach (var item in kvp)
                {
                    if (item.Status == oldStatus)
                    {
                        item.Status = newStatus;
                    }
                }
            }
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            foreach (var value in this._entities.Values)
            {
                foreach (var item in value)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
