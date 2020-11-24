namespace p01_ListyIterator
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ListyIterator<T> : IListyIterator<T>
    {
        private List<T> list;
        private int currentIndex;

        public ListyIterator(List<T> list)
        {
            this.list = list;
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
    }
}
