using System;
using System.Collections.Generic;
using System.Linq;

namespace p06_Truck_Tour
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var queue = new Queue<int[]>();
            for (int i = 0; i < n; i++)
            {
                var pump = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();
                queue.Enqueue(pump);
            }

            for (int i = 0; i < n; i++)
            {
                var isPassed = true;
                var fuel = 0;
                for (int j = 0; j < n; j++)
                {
                    var current = queue.Dequeue();
                    queue.Enqueue(current);

                    var fuelGiven = current[0];
                    var distance = current[1];

                    fuel += fuelGiven - distance;

                    if(fuel < 0)
                    {
                        i += j;
                        isPassed = false;
                        break;
                    }
                }

                if(isPassed)
                {
                    Console.WriteLine(i);
                    break;
                }
            }
        }
    }
}
