using System;
using System.Linq;

namespace BattlePoints
{
    class Program
    {
        static void Main(string[] args)
        {
            var enemiesEnergy = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var battlePoints = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var myEnergy = int.Parse(Console.ReadLine());

            var sack = new int[enemiesEnergy.Length + 1, myEnergy + 1];
            for (int row = 1; row < sack.GetLength(0); row++)
            {
                for (int energy = 1; energy < sack.GetLength(1); energy++)
                {
                    var enemyEnergy = enemiesEnergy[row - 1];
                    if (energy >= enemyEnergy)
                    {
                        var points = battlePoints[row - 1];
                        sack[row, energy] = Math.Max(sack[row - 1, energy], sack[row - 1, energy - enemyEnergy] + points);
                    }
                    else
                    {
                        sack[row, energy] = sack[row - 1, energy];
                    }
                }
            }

            Console.WriteLine(sack[sack.GetLength(0) - 1, sack.GetLength(1) - 1]);
        }
    }
}
