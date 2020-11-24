namespace p04_Froggy
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class Lake : IEnumerable<int>
    {
        public Lake(int[] stones)
        {
            this.Stones = stones;
        }

        public int[] Stones { get; private set; }

        public IEnumerator<int> GetEnumerator()
        {
            foreach (var stone in Stones)
            {
                yield return stone;
            }
        }

        public int this[int index]
        {
            get
            {
                return this.Stones[index];
            }

            set
            {
                this.Stones[index] = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
