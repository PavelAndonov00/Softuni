namespace p07_Custom_List
{
    using p07_Custom_List.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CustomList<T> : ICustomList<T>
        where T : IComparable<T>
    {
        private IList<T> list;

        public CustomList()
        {
            this.list = new List<T>();
        }

        public void Add(T element)
        {
            list.Add(element);
        }

        public T Remove(int index)
        {
            T removed = list[index];
            list.RemoveAt(index);

            return removed;
        }

        public bool Contains(T element)
        {
            return list.Contains(element);
        }

        public void Swap(int index1, int index2)
        {
            T first = list[index1];
            list[index1] = list[index2];
            list[index2] = first;
        }

        public int CountGreaterThan(T element)
        {
            int counter = 0;
            foreach (var elem in list)
            {
                if (elem.CompareTo(element) == 1)
                {
                    counter++;
                }
            }

            return counter;
        }

        public T Max()
        {
            return list.Max();
        }

        public T Min()
        {
            return list.Min();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, list);
        }
    }
}
