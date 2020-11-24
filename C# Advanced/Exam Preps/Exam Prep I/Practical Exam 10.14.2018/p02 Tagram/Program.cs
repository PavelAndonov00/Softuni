using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Tagram
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var users = new Dictionary<string, Dictionary<string, int>>();
            var tokens = new string[] { };
            while (input != "end")
            {
                if (input.Split().Length == 2)
                {
                    var usernameToBan = input
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray()[1];        
                    
                    if(users.ContainsKey(usernameToBan))
                    {
                        users.Remove(usernameToBan);
                    }

                    input = Console.ReadLine();
                    continue;
                }

                tokens = input
                    .Split(new string[] { " ->" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(e => e.Trim())
                    .ToArray();

                var username = tokens[0];
                var tag = tokens[1];
                var likes = int.Parse(tokens[2]);

                if(!users.ContainsKey(username))
                {
                    users[username] = new Dictionary<string, int>();
                }

                if (!users[username].ContainsKey(tag))
                {
                    users[username][tag] = 0;
                }

                users[username][tag] = likes;

                input = Console.ReadLine();
            }

            foreach (var firstDic in users.OrderByDescending(x => x.Value.Sum(e => e.Value))
                .ThenBy(x => x.Value.Count))
            {
                Console.WriteLine(firstDic.Key);
                foreach (var secondDic in firstDic.Value)
                {
                    Console.WriteLine($"- {secondDic.Key}: {secondDic.Value}");
                }
            }
        }
    }
}
