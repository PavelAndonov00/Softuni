namespace p06_Strategy_Pattern
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            SortedSet<Person> nameCompareSet = new SortedSet<Person>(new NameComparator());
            SortedSet<Person> ageCompareSet = new SortedSet<Person>(new AgeComparator());

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                int age = int.Parse(tokens[1]);

                nameCompareSet.Add(new Person(name, age));
                ageCompareSet.Add(new Person(name, age));
            }

            foreach (var person in nameCompareSet)
            {
                Console.WriteLine(person);
            }

            foreach (var person in ageCompareSet)
            {
                Console.WriteLine(person);
            }
        }
    }
}
