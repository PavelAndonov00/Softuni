using System;
using System.Collections.Generic;
using System.Linq;

namespace p07_The_V_logger
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var users = new Dictionary<string, Dictionary<string, List<string>>>();
            while (input != "Statistics")
            {
                var tokens = input
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    
                if(tokens.Length == 4)
                {
                    var user = tokens[0];
                   
                    if(!users.ContainsKey(user))
                    {
                        users[user] = new Dictionary<string, List<string>>();
                        users[user]["followers"] = new List<string>();
                        users[user]["following"] = new List<string>();
                    }                    
                }
                else
                {
                    var following = tokens[0];
                    var follower = tokens[2];

                    if(users.ContainsKey(following) && users.ContainsKey(follower) 
                        && following != follower && !users[follower]["followers"].Contains(following))
                    {
                        users[follower]["followers"].Add(following);
                        users[following]["following"].Add(follower);
                    }
                }
                input = Console.ReadLine();
            }

            Console.WriteLine($"The V-Logger has a total of {users.Count} vloggers in its logs.");

            var counter = 1;
            foreach (var user in users.OrderByDescending(x => x.Value["followers"].Count).ThenBy(x => x.Value["following"].Count))
            {
                Console.WriteLine($"{counter}. {user.Key} : {user.Value["followers"].Count} followers, {users[user.Key]["following"].Count} following");

                if (user.Value["followers"].Count > 0 && counter == 1)
                {                    
                    Console.WriteLine(String.Join("\n", user.Value["followers"].OrderBy(e => e).Select(e => e = "*  " + e)));
                }

                counter++;
            }
        }
    }
}
