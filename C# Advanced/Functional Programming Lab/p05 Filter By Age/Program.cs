using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_Filter_By_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var people = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split(", ")
                    .ToArray();

                var name = input[0];
                var age = int.Parse(input[1]);

                people.Add(new KeyValuePair<string, int>(name, age));
            }

            var filter = Console.ReadLine();
            var ageFilter = int.Parse(Console.ReadLine());

            if(filter == "older")
            {
                people = people
                    .Where(e => e.Value >= ageFilter)
                    .ToList();
            }
            else
            {
                people = people
                    .Where(e => e.Value < ageFilter)
                    .ToList();
            }

            var printCriteria = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if(printCriteria.Count > 1)
            {                    
                people.ForEach(e => Console.WriteLine($"{e.Key} - {e.Value}"));
            }
            else
            {
                switch (printCriteria[0])
                {
                    case "name":
                        people.ForEach(e => Console.WriteLine($"{e.Key}"));
                        break;
                    default:
                        people.ForEach(e => Console.WriteLine($"{e.Value}"));
                        break;
                }
            }
        }
    }
}
