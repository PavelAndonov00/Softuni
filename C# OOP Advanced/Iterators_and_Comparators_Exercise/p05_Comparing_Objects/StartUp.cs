namespace p05_Comparing_Objects
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            string input = Console.ReadLine();
            while (input != "END")
            {
                string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string town = tokens[2];

                people.Add(new Person(name, age, town));

                input = Console.ReadLine();
            }

            int nthPerson = int.Parse(Console.ReadLine());
            Person wantedPerson = people[nthPerson - 1];
            int equalTo = 0;
            int notEqualTo = 0;
            for (int i = 0; i < people.Count; i++)
            {
                if (wantedPerson.CompareTo(people[i]) == 0)
                {
                    equalTo++;
                }
                else
                {
                    notEqualTo++;
                }
            }

            if (equalTo <= 1)
            {
                Console.WriteLine("No matches");
            }
            else
            {
                Console.WriteLine($"{equalTo} {notEqualTo} {people.Count}");
            }
        }
    }
}
