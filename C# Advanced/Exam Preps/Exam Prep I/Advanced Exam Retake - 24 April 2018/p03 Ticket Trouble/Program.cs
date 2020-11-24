using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace p03_Ticket_Trouble
{
    class Program
    {
        static void Main(string[] args)
        {
            var location = Console.ReadLine();
               
            var suitcase = Console.ReadLine();

            var seatNumbers = new List<string>();

            var firstRegex = new Regex(@"\[[^\[]*{" + location + @"}[^\[]*{([A-Z]\d{1,2})}[^\[]*\]");
            var firstMatches = firstRegex.Matches(suitcase);
            foreach (Match match in firstMatches)
            {
                seatNumbers.Add(match.Groups[1].ToString());
            }

            var secondRegex = new Regex(@"{[^{]*\[" + location + @"\][^{]*\[([A-Z]\d{1,2})\][^{]*}");
            var secondMatches = secondRegex.Matches(suitcase);
            foreach (Match match in secondMatches)
            {
                seatNumbers.Add(match.Groups[1].ToString());
            }

            if (seatNumbers.Count > 2)
            {
                var correctSeatNumbers = new List<string>();
                foreach (var seatNumber in seatNumbers)
                {
                    foreach (var sN in seatNumbers)
                    {
                        var first = seatNumber.Substring(1);
                        var second = sN.Substring(1);
                        if (first == second && seatNumber != sN)
                        {
                            correctSeatNumbers.Add(seatNumber);
                            correctSeatNumbers.Add(sN);
                        }
                    }
                }

                Console.WriteLine($"You are traveling to {location} on seats {correctSeatNumbers[0]} and {correctSeatNumbers[1]}.");
            }
            else
            {
                Console.WriteLine($"You are traveling to {location} on seats {seatNumbers[0]} and {seatNumbers[1]}.");
            }

        }
    }
}
