using System;
using System.Collections.Generic;
using System.Linq;

namespace CableMerchant
{
    class Program
    {
        static int[] prices;
        static int[] cableCuts;
        static int[] bestPrices;
        static int connector;
        static int counter;
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();
            var connectorPrice = int.Parse(Console.ReadLine());
            connector = connectorPrice * 2;
            Initialize(input);

            GetBestPrices(connectorPrice);
            Console.WriteLine(string.Join(" ", prices.Skip(1)));
            Console.WriteLine(string.Join(" ", bestPrices.Skip(1)));
            //Console.WriteLine("Steps: " + counter);
        }

        private static void GetBestPrices(int connectorPrice)
        {
            var bestPrices = new int[prices.Length];
            var cableCuts = new int[prices.Length];
            for (int length = 1; length < prices.Length; length++)
            {
                var bestPrice = FindBestPrice(length);

                //var cuts = 1;
                //var tempLength = length;
                //while (cableCuts[tempLength] != 0)
                //{
                //    cuts++;
                //    tempLength -= cableCuts[tempLength];
                //}

                //if (cuts > 1)
                //{
                //    bestPrice -= connectorPrice * (cuts + cuts / 2);
                //}

                if (bestPrice > prices[length])
                {
                    prices[length] = bestPrice;
                }
            }
        }

        private static void Initialize(int[] input)
        {
            prices = new int[input.Length + 1];
            for (int i = 1; i < prices.Length; i++)
            {
                prices[i] = input[i - 1];
            }
            cableCuts = new int[input.Length + 1];
            bestPrices = new int[input.Length + 1];
        }

        private static int FindBestPrice(int cableLength)
        {
            counter++;
            if (cableLength == 0)
            {
                return 0;
            }

            if(bestPrices[cableLength] > 0)
            {
                return bestPrices[cableLength];
            }

            var bestPrice = prices[cableLength];
            bestPrices[cableLength] = bestPrice;
            for (int length = 1; length < cableLength; length++)
            {
                var currentSum = prices[length] + FindBestPrice(cableLength - length);
                currentSum -= connector;
                if (currentSum > bestPrice)
                {
                    bestPrice = currentSum;
                    bestPrices[cableLength] = bestPrice;
                    cableCuts[cableLength] = length;
                }
            }
            
            return bestPrices[cableLength];
        }
    }
}
