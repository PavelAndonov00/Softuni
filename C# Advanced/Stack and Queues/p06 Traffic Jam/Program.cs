using System;
using System.Collections.Generic;

namespace p06_Traffic_Jam
{
    class Program
    {
        static void Main(string[] args)
        {
            var carOnGreen = int.Parse(Console.ReadLine());
            var queue = new Queue<string>();
            var carPassed = 0;

            var input = Console.ReadLine();
            while (input != "end")
            {
                if(input == "green")
                {
                    var carThatCanPass = Math.Min(carOnGreen, queue.Count);
                    for (int i = 0; i < carThatCanPass; i++)
                    {
                        Console.WriteLine(queue.Dequeue() + " passed!");
                        carPassed++;
                    }
                } else
                {
                    queue.Enqueue(input);
                }
     
                input = Console.ReadLine();
            }

            Console.WriteLine(carPassed + " cars passed the crossroads.");
        }
    }
}
