using System;
using System.Collections.Generic;
using System.Linq;

namespace p11_Poisonous_Plants
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = int.Parse(Console.ReadLine());

            var plants = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            
            var isDying = true;
            var day = 0;
            while (isDying)
            {
                day++;
                var diedPlantsIndexes = new List<int>();
                for (int i = 1; i < plants.Length; i++)
                {
                    if(plants[i-1] < plants[i])
                    {
                        diedPlantsIndexes.Add(i);
                    }
                }
                
                if (diedPlantsIndexes.Count == 0)
                {
                    isDying = false;
                }
                else
                {
                    plants = plants.Where((e, i) => !diedPlantsIndexes.Contains(i)).ToArray();
                }

            }

            Console.WriteLine(day - 1);
        }
    }
}
