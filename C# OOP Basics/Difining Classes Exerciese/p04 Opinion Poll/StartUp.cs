using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var people = new List<Person>();

            var n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();

                var name = input[0];
                var age = int.Parse(input[1]);

                if(age > 30)
                {
                    people.Add(new Person(name, age));
                }
            }

            people
                .OrderBy(x => x.Name)
                .Select(e => $"{e.Name} - {e.Age}")
                .ToList()
                .ForEach(Console.WriteLine);
            
        }
    }
}
