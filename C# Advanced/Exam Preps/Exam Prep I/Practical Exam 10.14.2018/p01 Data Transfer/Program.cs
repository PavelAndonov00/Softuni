using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace p01_Data_Transfer
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            var validMessage = new Regex(@"s:([^;]*);r:([^;]*);m--(""[A-Za-z ]+"")");
            var onlyLettersRegex = new Regex("[A-Za-z]+");
            var onlyDigitsRegex = new Regex("\\d");
            var dataSize = 0;
            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine();

                if (validMessage.IsMatch(input))
                {
                    var matches = validMessage.Matches(input);
                    foreach (Match match in matches)
                    {
                        var senderDigits = onlyDigitsRegex.Matches(match.Groups[1].ToString());
                        foreach (var digit in senderDigits)
                        {
                            dataSize += int.Parse(digit.ToString());
                        }

                        var recieverDigits = onlyDigitsRegex.Matches(match.Groups[2].ToString());
                        foreach (var digit in recieverDigits)
                        {
                            dataSize += int.Parse(digit.ToString());
                        }

                        var senderArr = match.Groups[1].ToString().Split();
                        var sender = "";
                        foreach (var part in senderArr)
                        {
                            sender += String.Join("", onlyLettersRegex.Matches(part.ToString())) + " ";
                        }
                        sender = sender.Trim();

                        var recieverArr = match.Groups[2].ToString().Split();
                        var reciever = "";
                        foreach (var part in recieverArr)
                        {
                            reciever += String.Join("", onlyLettersRegex.Matches(part.ToString())) + " ";
                        }
                        reciever = reciever.Trim();


                        var message = match.Groups[3].ToString();

                        Console.WriteLine($@"{sender} says {message} to {reciever}");
                    }
                }
            }

            Console.WriteLine($"Total data transferred: {dataSize}MB");
        }
    }
}
