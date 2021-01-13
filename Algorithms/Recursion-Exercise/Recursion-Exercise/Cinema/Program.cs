using System;
using System.Collections.Generic;
using System.Linq;

namespace Cinema
{
    class Program
    {
        static string[] watchers;
        static Dictionary<int, string> reservedPlaces;
        static string[] watchersWithoutReservedPlaces;
        static void Main(string[] args)
        {
            watchers = Console.ReadLine()
                .Split(", ");

            reservedPlaces = new Dictionary<int, string>();
            var input = Console.ReadLine();
            while (input != "generate")
            {
                var tokens = input.Split(" - ");
                var name = tokens[0];
                var place = int.Parse(tokens[1]);
                reservedPlaces[place] = name;

                input = Console.ReadLine();
            }

            
            watchersWithoutReservedPlaces = watchers
                .Where(w => !reservedPlaces.Values.Contains(w))
                .ToArray();
            GetPermutations(0);
        }

        private static void GetPermutations(int index)
        {
            if(index >= watchersWithoutReservedPlaces.Length)
            {
                var permIndex = 0;
                for (int i = 0; i < watchers.Length; i++)
                {
                    var reservedPlaceIndex = i + 1;
                    if (reservedPlaces.Keys.Contains(reservedPlaceIndex))
                    {
                        Console.Write(reservedPlaces[reservedPlaceIndex]);
                    }
                    else
                    {
                        Console.Write(watchersWithoutReservedPlaces[permIndex]);
                        permIndex++;
                    }

                    if (i != watchers.Length - 1)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
                return;
            }

            GetPermutations(index + 1);

            for (int i = index + 1; i < watchersWithoutReservedPlaces.Length; i++)
            {
                Swap(index, i);
                GetPermutations(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(int first, int second)
        {
            var temp = watchersWithoutReservedPlaces[first];
            watchersWithoutReservedPlaces[first] = watchersWithoutReservedPlaces[second];
            watchersWithoutReservedPlaces[second] = temp;
        }
    }
}
