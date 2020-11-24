using System;
using System.Collections.Generic;

namespace p06_Wardrobe
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var wardrobe = new Dictionary<string, Dictionary<string, int>>();
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine()
                    .Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);

                var color = input[0];
                var clothes = input[1]
                    .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                if (!wardrobe.ContainsKey(color))
                {
                    wardrobe[color] = new Dictionary<string, int>();
                }

                foreach (var cl in clothes)
                {
                    if (!wardrobe[color].ContainsKey(cl))
                    {
                        wardrobe[color][cl] = 0;
                    }
                    wardrobe[color][cl]++;
                }
            }

            var clothToLookFor = Console.ReadLine()
                 .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var key in wardrobe.Keys)
            {
                Console.WriteLine(key + " clothes:");                               
                foreach (var cloth in wardrobe[key].Keys)
                {
                    if(key == clothToLookFor[0] && cloth == clothToLookFor[1])
                    {
                        Console.WriteLine("* " + cloth + " - " + wardrobe[key][cloth] + " (found!)");
                    }
                    else
                    {
                        Console.WriteLine("* " + cloth + " - " + wardrobe[key][cloth]);
                    }
                    
                    
                }
            }
        }
    }
}
