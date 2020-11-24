using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace p04_Cups_and_Bottles
{
    class Program
    {
        static void Main(string[] args)
        {
            var cupsCapicity = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var cups = new Queue<int>(cupsCapicity);

            var bottlesCapicity = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var bottles = new Stack<int>(bottlesCapicity);

            var currentCup = cups.Dequeue();
            var currentBottle = bottles.Pop();
            var wastedWater = 0;
            while (true)
            {
                if(currentBottle >= currentCup)
                {
                    wastedWater += currentBottle - currentCup;

                    if(cups.Count == 0 || bottles.Count == 0)
                    {
                        break;
                    }
                    currentCup = cups.Dequeue();
                    currentBottle = bottles.Pop();
                }
                else
                {
                    currentCup -= currentBottle;

                    if (bottles.Count == 0)
                    {
                        break;
                    }
                    currentBottle = bottles.Pop();
                }
            }

            if(cups.Count > 0)
            {
                Console.WriteLine("Cups: " + String.Join(" ", cups));
            }
            else
            {
                Console.WriteLine("Bottles: " + String.Join(" ", bottles));
            }

            Console.WriteLine($"Wasted litters of water: {wastedWater}");
        }
    }
}
