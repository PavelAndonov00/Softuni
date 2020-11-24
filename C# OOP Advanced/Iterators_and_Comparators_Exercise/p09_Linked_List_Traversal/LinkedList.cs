namespace p09_Linked_List_Traversal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class LinkedList<T> : IEnumerable<T>
    {
        private T[] array;
        private int currentIndex;

        public LinkedList()
        {
            this.array = new T[4];
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (var item in array.Where(e => !e.Equals(default(T))))
                {
                    count++;
                }

                return count;
            }
        }


        private void Resize()
        {
            T[] extended = new T[this.array.Length * 2];

            for (int i = 0; i < this.array.Length; i++)
            {
                extended[i] = this.array[i];
            }

            this.array = extended;
        }

        public void Add(T item)
        {
            if (currentIndex == this.array.Length - 1)
            {
                this.Resize();
                currentIndex++;
            }

            this.array[currentIndex] = item;
            currentIndex++;
        }

        public bool Remove(T item)
        {
            bool isThereSuchItem = false;
            for (int i = 0; i < this.array.Length; i++)
            {
                if (this.array[i].Equals(item))
                {
                    this.array[i] = default(T);
                    isThereSuchItem = true;
                    break;
                }
            }

            this.array = this.array.Where(e => !e.Equals(default(T))).ToArray();
            currentIndex = this.array.Length - 1;
            return isThereSuchItem;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in array.Where(e => !e.Equals(default(T))))
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
