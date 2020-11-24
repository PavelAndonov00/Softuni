using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_Hot_Potato
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = Console.ReadLine().Split(' ');
            var nthToss = int.Parse(Console.ReadLine());

            var queue = new Queue<string>(players);

            while (queue.Count > 1)
            {
                for (int i = 1; i < nthToss; i++)
                {
                    queue.Enqueue(queue.Dequeue());
                }

                Console.WriteLine($"Removed {queue.Dequeue()}");
            }

            Console.WriteLine($"Last is {queue.Dequeue()}");
        }
    }
}
