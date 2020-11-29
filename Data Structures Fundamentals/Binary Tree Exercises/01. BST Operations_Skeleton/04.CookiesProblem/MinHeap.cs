namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MinHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => _elements.Count;

        public T Dequeue()
        {
            if(this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            var element = this._elements[0];
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);
            return element;
        }

        private void HeapifyDown(int index)
        {
            while (true)
            {
                var swap = -1;
                var leftChildIndex = index * 2 + 1;
                var rightChildIndex = index * 2 + 2;

                if (this.IsValid(leftChildIndex) && this.IsLessThan(leftChildIndex, index))
                {
                    swap = leftChildIndex;
                }

                if (this.IsValid(rightChildIndex) && this.IsLessThan(rightChildIndex, leftChildIndex))
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

        private bool IsValid(int index)
        {
            if(index > 0 && index < this.Size)
            {
                return true;
            }

            return false;
        }

        public void Add(T element)
        {
            this._elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);
            while (this.IsLessThan(index, parentIndex))
            {
                this.Swap(index, parentIndex);

                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private void Swap(int index, int parentIndex)
        {
            var current = this._elements[index];
            this._elements[index] = this._elements[parentIndex];
            this._elements[parentIndex] = current;
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        private bool IsLessThan(int index1, int index2)
        {
            return this._elements[index1].CompareTo(this._elements[index2]) == -1;
        }

        public T Peek()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            return this._elements[0];
        }
    }
}
