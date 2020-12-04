using _02.Data.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02.Data.Models
{
    public class PriorityQueue
    {
        private List<IEntity> _elements;

        public PriorityQueue()
        {
            this._elements = new List<IEntity>();
        }

        public int Size => this._elements.Count;

        public IEntity Dequeue()
        {
            IEntity firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            _elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);
            return firstElement;
        }

        public void Add(IEntity element)
        {
            this._elements.Add(element);
            HeapifyUp(this.Size - 1);
        }

        public IEntity Peek()
        {
            return this._elements[0];
        }

        public List<IEntity> GetAll()
        {
            return this._elements.ToList();
        }

        public List<IEntity> GetAllByType(string type)
        {
            var result = new List<IEntity>();
            foreach (var item in this._elements)
            {
                if (item.GetType().Name == type)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        internal IEntity GetById(int id)
        {
            foreach (var item in this._elements)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public List<IEntity> GetChildrenByParentId(int parentId)
        {
            List<IEntity> result = new List<IEntity>();
            foreach (var item in this._elements)
            {
                if (item.ParentId == parentId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                var swap = -1;
                var leftChildIndex = this.GetLeftChildIndex(index);
                if (!IsOutOfBound(leftChildIndex) && this.IsGreater(leftChildIndex, index))
                {
                    swap = leftChildIndex;
                }

                var rightChildIndex = leftChildIndex + 1;
                if (!IsOutOfBound(rightChildIndex) && this.IsGreater(rightChildIndex, leftChildIndex))
                {
                    swap = rightChildIndex;
                }

                if (swap == -1)
                {
                    break;
                }
                this.Swap(swap, index);
                index = swap;
            }
        }

        private bool IsOutOfBound(int index)
        {
            if (index < 0 || index > this.Size - 1)
            {
                return true;
            }

            return false;
        }

        private int GetLeftChildIndex(int index)
        {
            return index * 2 + 1;
        }

        private void HeapifyUp(int childIndex)
        {
            var parentIndex = this.GetParentIndex(childIndex);
            while (childIndex > 0 && this.IsGreater(childIndex, parentIndex))
            {
                this.Swap(childIndex, parentIndex);
                childIndex = parentIndex;
                parentIndex = this.GetParentIndex(childIndex);
            }
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private bool IsGreater(int elementIndex, int parentIndex)
        {
            return this._elements[elementIndex].CompareTo(this._elements[parentIndex]) < 0;
        }

        private void Swap(int elementIndex, int parentIndex)
        {
            var child = this._elements[elementIndex];
            this._elements[elementIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = child;
        }
    }
}

