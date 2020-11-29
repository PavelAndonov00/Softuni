namespace _04.CookiesProblem
{
    using _03.MinHeap;
    using System;
    using System.Collections.Generic;

    public class CookiesProblem
    {

        public int Solve(int k, int[] cookies)
        {
            var heap = new MinHeap<int>();
            foreach (var cookie in cookies)
            {
                heap.Add(cookie);
            }

            var steps = 0;
            var current = heap.Dequeue();
            while (current < k && heap.Size > 0)
            {
                //Get second least sweet
                var second = heap.Dequeue();
                //Create mixed cookie
                var newOne = current + second * 2;
                heap.Add(newOne);
                steps++;
                current = heap.Dequeue();
            }

            return current < k ? -1 : steps;
        }
    }
}
