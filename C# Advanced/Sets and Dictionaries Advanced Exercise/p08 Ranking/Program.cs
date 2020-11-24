using System;
using System.Collections.Generic;
using System.Linq;

namespace p08_Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var contestsByPassword = new Dictionary<string, string>();
            while (input != "end of contests")
            {
                var tokens = input
                    .Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var contest = tokens[0];
                var password = tokens[1];

                if (!contestsByPassword.ContainsKey(contest))
                {
                    contestsByPassword[contest] = password;
                }

                input = Console.ReadLine();
            }


            var usersByContests = new Dictionary<string, Dictionary<string, double>>();
            input = Console.ReadLine();
            while (input != "end of submissions")
            {
                var tokens = input
                    .Split(new string[] { "=>" }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var contest = tokens[0];
                var password = tokens[1];
                var user = tokens[2];
                var points = double.Parse(tokens[3]);

                if(contestsByPassword.ContainsKey(contest))
                {
                    if (contestsByPassword[contest] == password)
                    {
                        if (!usersByContests.ContainsKey(user))
                        {
                            usersByContests[user] = new Dictionary<string, double>();
                        }

                        if (!usersByContests[user].ContainsKey(contest))
                        {
                            usersByContests[user][contest] = points;
                        }
                        else
                        {
                            if (usersByContests[user][contest] < points)
                            {
                                usersByContests[user][contest] = points;
                            }
                        }
                    }
                }
                
                input = Console.ReadLine();
            }

            foreach (var user in usersByContests.OrderByDescending(e => e.Value.Values.Sum()))
            {
                Console.WriteLine($"Best candidate is {user.Key} with total {user.Value.Values.Sum()} points.");
                break;
            }

            Console.WriteLine("Ranking:");
            foreach (var user in usersByContests.OrderBy(k => k.Key))
            {
                Console.WriteLine(user.Key);
                foreach (var contest in user.Value.OrderByDescending(v => v.Value))
                {
                    Console.WriteLine($"#  {contest.Key} -> {contest.Value}");
                }
            }
           
        }
    }
}
