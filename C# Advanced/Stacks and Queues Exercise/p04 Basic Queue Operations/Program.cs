using System;
using System.Collections.Generic;
using System.Linq;

namespace p04_Basic_Queue_Operations
{
    class Program
    {
        static void Main(string[] args)
        {
            var operations = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var numbersToPush = operations[0];
            var numbersToDequeue = operations[1];
            var numberToLookFor = operations[2];

            var numbers = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray();

            var queue = new Queue<int>(numbers.Take(numbersToPush));
            for (int i = 0; i < numbersToDequeue; i++)
            {
                queue.Dequeue();
            }

            if(queue.Count == 0)
            {
                Console.WriteLine(0);
            }
            else if(queue.Contains(numberToLookFor))
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine(queue.Min());
            }
        }
    }
}
