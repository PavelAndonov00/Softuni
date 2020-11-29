namespace _03.PriorityQueue
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            this.CheckIfEmpty();
            T firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            _elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);
            return firstElement;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            this.CheckIfEmpty();

            return this._elements[0];
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                var swap = -1;
                var leftChildIndex = this.GetLeftChildIndex(index);
                if(!IsOutOfBound(leftChildIndex) && this.IsGreater(leftChildIndex, index))
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
            if(index < 0 || index > this.Size - 1)
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
            return this._elements[elementIndex].CompareTo(this._elements[parentIndex]) == 1;
        }

        private void Swap(int elementIndex, int parentIndex)
        {
            var child = this._elements[elementIndex];
            this._elements[elementIndex] = this._elements[parentIndex];
            this._elements[parentIndex] = child;
        }

        private void CheckIfEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Empty heap");
            }
        }
    }
}
