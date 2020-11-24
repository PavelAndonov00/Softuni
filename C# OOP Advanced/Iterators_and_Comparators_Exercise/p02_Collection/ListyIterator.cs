namespace p02_Collection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class ListyIterator<T> : IListyIterator<T>, IEnumerable<T>
    {
        private List<T> list;
        private int currentIndex;

        public ListyIterator(List<T> list)
        {
            this.list = list;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in list)
            {
                yield return item;
            }
        }

        public bool HasNext()
        {
            return currentIndex + 1 < list.Count;
        }

        public bool Move()
        {
            if (!this.HasNext())
            {
                return false;
            }

            currentIndex++;
            return true;
        }

        public void Print()
        {
            if(list.Count == 0)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            Console.WriteLine(list[currentIndex]);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
