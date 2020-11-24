using System;
using System.Collections.Generic;
using System.Linq;

namespace p06_Parking_Lot
{
    class Program
    {
        static void Main(string[] args)
        {
            var parking = new HashSet<string>();

            var input = Console.ReadLine();
            while (input != "END")
            {
                var tokens = input
                    .Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var direction = tokens[0];
                var carNumber = tokens[1];

                switch (direction)
                {
                    case "IN":
                        parking.Add(carNumber);
                        break;
                    default:
                        parking.Remove(carNumber);
                        break;                        
                }

                input = Console.ReadLine();
            }

            if(parking.Count == 0)
            {
                Console.WriteLine("Parking Lot is Empty");
            }
            else
            {
                foreach (var car in parking)
                {
                    Console.WriteLine(car);
                }
            }
        }
    }
}
