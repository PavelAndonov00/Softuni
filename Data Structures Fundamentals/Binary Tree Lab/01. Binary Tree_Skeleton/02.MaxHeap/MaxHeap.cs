namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MaxHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => elements.Count;

        public void Add(T element)
        {
            elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            var max = this.GetMaxElement();
            return max;
        }


        private void HeapifyUp(int index)
        {
            var parentIndex = this.GetParentIndex(index);
            while (index > 0 && this.IsGreaterThan(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = this.GetParentIndex(index);
            }
        }

        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        protected bool IsGreaterThan(int index, int parentIndex)
        {
            if(this.elements[index].CompareTo(this.elements[parentIndex]) == 1)
            {
                return true;
            }

            return false;
        }

        protected void Swap(int index, int parentIndex)
        {
            var child = this.elements[index];
            this.elements[index] = this.elements[parentIndex];
            this.elements[parentIndex] = child;
        }

        private T GetMaxElement()
        {
            if(this.Size > 0)
            {
                return this.elements[0];
            }

            throw new InvalidOperationException("Empty heap");
        }
    }
}
