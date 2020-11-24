namespace p03_Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Stack<T> : IEnumerable<T>, IStack<T>
    {
        private T[] array;
        private int currentIndex;

        public Stack()
        {
            this.array = new T[4];
            this.currentIndex = -1;
        }

        private void Resize()
        {
            T[] extended = new T[this.array.Length * 2];

            for (int i = 0; i < this.array.Length; i++)
            {
                extended[i] = array[i];
            }

            array = extended;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = currentIndex; i >= 0; i--)
            {
                yield return array[i];
            }
        }

        public void Pop()
        {
            if(currentIndex < 0)
            {
                throw new InvalidOperationException("No elements");
            }

            currentIndex--;
        }

        public void Push(params T[] paramsArr)
        {
            foreach (var item in paramsArr)
            {
                if(array.Length == currentIndex + 1)
                {
                    Resize();
                }

                currentIndex++;
                array[currentIndex] = item;
            }
        } 

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
