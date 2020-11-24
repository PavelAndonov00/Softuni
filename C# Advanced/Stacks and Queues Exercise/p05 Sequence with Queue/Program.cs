using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_Sequence_with_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            var s1 = long.Parse(Console.ReadLine());

            var queue = new Queue<long>();
            queue.Enqueue(s1);

            var result = new List<long>();
            result.Add(s1);

            while (result.Count < 50)
            {
                var current = queue.Dequeue();
                queue.Enqueue(current + 1);
                queue.Enqueue(2 * current + 1);
                queue.Enqueue(current + 2);

                result.Add(current + 1);
                result.Add(2 * current + 1);
                result.Add(current + 2);
            }

            result = result.Take(50).ToList();
            Console.WriteLine(String.Join(" ", result));
        }
    }
}
