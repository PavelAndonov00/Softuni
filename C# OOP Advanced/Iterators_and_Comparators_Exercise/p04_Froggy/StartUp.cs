namespace p04_Froggy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            int[] stones = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            Lake lake = new Lake(stones);

            List<int> evenStones = new List<int>();
            List<int> oddStones = new List<int>();

            for (int i = 0; i < lake.Count(); i++)
            {
                if(i % 2 == 0)
                {
                    evenStones.Add(lake[i]);
                }
                else
                {
                    oddStones.Add(lake[i]);
                }
            }

            int[] concatedCollection =
                evenStones
                .Concat(oddStones.ToArray().Reverse())
                .ToArray();
            Console.WriteLine(string.Join(", ", concatedCollection));
        }
    }
}
