using System;
using System.Collections.Generic;
using System.Linq;

namespace p04_Cities_by_Continent_and_Country
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var dic = new Dictionary<string, Dictionary<string, List<string>>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split()
                    .ToArray();

                var continent = input[0];
                var country = input[1];
                var city = input[2];

                if(!dic.ContainsKey(continent))
                {
                    dic[continent] = new Dictionary<string, List<string>>();               
                }

                if(!dic[continent].ContainsKey(country))
                {
                    dic[continent][country] = new List<string>();
                }

                dic[continent][country].Add(city);
            }

            foreach (var continent in dic.Keys)
            {
                Console.WriteLine(continent + ":");
                foreach (var country in dic[continent].Keys)
                {
                    Console.WriteLine("  " + country + " -> " + String.Join(", ",dic[continent][country]));
                }
            }
        }
    }
}
