using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main(string[] args)
    {
        var name = Console.ReadLine();
        var person = new Person(name);

        var people = new List<Person>();
        var log = new List<string>();
        var input = Console.ReadLine();
        while (input != "End")
        {
            var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (!Char.IsDigit(tokens[0][0]) && !Char.IsDigit(tokens[1][0]) && Char.IsDigit(tokens[2][0]))
            {
                people.Add(new Person(tokens[0] + " " + tokens[1], tokens[2]));
            }
            else if (!Char.IsDigit(tokens[0][0]) && !Char.IsDigit(tokens[1][0]) && tokens[2] == "-" &&
                !Char.IsDigit(tokens[3][0]) && !Char.IsDigit(tokens[4][0]))
            {
                log.Add(tokens[0] + " " + tokens[1] + "-" + tokens[3] + " " + tokens[4]);
            }
            else if (Char.IsDigit(tokens[0][0]) && tokens[1] == "-" && Char.IsDigit(tokens[2][0]))
            {
                log.Add(tokens[0] + "-" + tokens[2]);
            }
            else if (!Char.IsDigit(tokens[0][0]) && !Char.IsDigit(tokens[1][0]) && tokens[2] == "-" &&
                Char.IsDigit(tokens[3][0]))
            {
                log.Add(tokens[0] + " " + tokens[1] + "-" + tokens[3]);
            }
            else if (Char.IsDigit(tokens[0][0]) && tokens[1] == "-" &&
                !Char.IsDigit(tokens[2][0]) && !Char.IsDigit(tokens[3][0]))
            {
                log.Add(tokens[0] + "-" + tokens[2] + " " + tokens[3]);
            }

            input = Console.ReadLine();
        }

        if (name.Contains("/"))
        {
            person = people.Find(p => p.birthday == name);
        }
        else
        {
            person = people.Find(p => p.name == name);
        }

        foreach (var pair in log)
        {
            var tokens = pair.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var first = tokens[0];
            var second = tokens[1];
            if (first == person.birthday || first == person.name)
            {
                if (Char.IsDigit(second[0]))
                {
                    var current = people.Find(p => p.birthday == second);
                    if (current != null)
                    {
                        person.children.Add(current.name + " " + current.birthday);
                    }
                }
                else
                {
                    var current = people.Find(p => p.name == second);
                    if (current != null)
                    {
                        person.children.Add(current.name + " " + current.birthday);
                    }
                }
            }
            else if (second == person.birthday || second == person.name)
            {
                if (Char.IsDigit(first[0]))
                {
                    var current = people.Find(p => p.birthday == first);
                    if (current != null)
                    {
                        person.parents.Add(current.name + " " + current.birthday);
                    }
                }
                else
                {
                    var current = people.Find(p => p.name == first);
                    if (current != null)
                    {
                        person.parents.Add(current.name + " " + current.birthday);
                    }
                }

            }
        }

        Console.WriteLine(person.ToString());
    }
}

