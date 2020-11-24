using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace p01_Crossroads
{
    class Program
    {
        static void Main(string[] args)
        {
            var greenLightDuration = int.Parse(Console.ReadLine());
            var freeWindowDuration = int.Parse(Console.ReadLine());

            var cars = new Queue<string>();
            var input = Console.ReadLine();
            var carPassed = 0;
            while (input != "END")
            {
                if(input != "green")
                {
                    cars.Enqueue(input);
                }
                else
                {
                    var currentTime = greenLightDuration;
                    while (true)
                    {
                        if(cars.Count == 0 || currentTime == 0)
                        {
                            break;
                        }

                        var current = cars.Dequeue();                       
                        if(currentTime - current.Length >= 0)
                        {
                            currentTime -= current.Length;
                        }
                        else
                        {
                            var substingedCar = current.Substring(currentTime);
                            currentTime = 0;
                            if(freeWindowDuration - substingedCar.Length < 0)
                            {
                                var characterHit = substingedCar.Substring(freeWindowDuration).First().ToString();

                                Console.WriteLine("A crash happened!");
                                Console.WriteLine($"{current} was hit at {characterHit}.");
                                Environment.Exit(0);
                            }                           
                        }

                        carPassed++;
                    }
                }

                input = Console.ReadLine();
            }

            Console.WriteLine("Everyone is safe.");
            Console.WriteLine($"{carPassed} total cars passed the crossroads.");
        }
    }
}
