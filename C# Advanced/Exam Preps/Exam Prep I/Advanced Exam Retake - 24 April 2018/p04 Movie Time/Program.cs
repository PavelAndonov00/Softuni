using System;
using System.Collections.Generic;
using System.Linq;

namespace p02_Knight_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            var desireGenre = Console.ReadLine();
            var desireDuration = Console.ReadLine();

            var movies = new Dictionary<string, Dictionary<string, string>>();
            var input = Console.ReadLine();
            while (input != "POPCORN!")
            {
                var tokens = input
                    .Split("|", StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var movie = tokens[0];
                var genre = tokens[1];
                var duration = tokens[2];

                if (!movies.ContainsKey(movie))
                {
                    movies[movie] = new Dictionary<string, string>();
                }

                movies[movie]["genre"] = genre;
                movies[movie]["duration"] = duration;

                input = Console.ReadLine();
            }

            var totalDuration = new TimeSpan();
            foreach (var firstDic in movies)
            {
                var current = TimeSpan.Parse(firstDic.Value["duration"]);
                totalDuration += current;
            }

            var sortedDic = new Dictionary<string, Dictionary<string, string>>();
            input = Console.ReadLine();
            if (desireDuration == "Short")
            {
                sortedDic = movies
                    .OrderBy(e => TimeSpan.Parse(e.Value["duration"]))
                    .ThenBy (e => e.Key)
                    .ToDictionary(x => x.Key, y => y.Value);                
            }
            else
            {
                sortedDic = movies.OrderByDescending(e => TimeSpan.Parse(e.Value["duration"]))
                    .ToDictionary(x => x.Key, y => y.Value);                
            }

            foreach (var firstDic in sortedDic)
            {
                if (input == "Yes" && firstDic.Value["genre"] == desireGenre)
                {
                    Console.WriteLine(firstDic.Key);
                    Console.WriteLine($"We're watching {firstDic.Key} - {firstDic.Value["duration"]}");
                    Console.WriteLine($"Total Playlist Duration: {totalDuration}");
                    break;
                }

                if(input != "Yes" && firstDic.Value["genre"] == desireGenre)
                {
                    Console.WriteLine(firstDic.Key);
                    input = Console.ReadLine();
                }
            }
        }
    }
}
