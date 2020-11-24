using System;
using System.Collections.Generic;

namespace p07_SoftUni_Party
{
    class Program
    {
        static void Main(string[] args)
        {
            var vipGuests = new HashSet<string>();
            var regularGuests = new HashSet<string>();

            var input = Console.ReadLine();
            while (input != "PARTY")
            {
                if (Char.IsDigit(input[0]))
                {
                    vipGuests.Add(input);
                }
                else
                {
                    regularGuests.Add(input);
                }

                input = Console.ReadLine();
            }

            input = Console.ReadLine();
            while (input != "END")
            {
                if (vipGuests.Contains(input))
                {
                    vipGuests.Remove(input);
                }
                else if (regularGuests.Contains(input))
                {
                    regularGuests.Remove(input);
                }

                input = Console.ReadLine();
            }


            Console.WriteLine(vipGuests.Count + regularGuests.Count);
            foreach (var vipGuest in vipGuests)
            {
                Console.WriteLine(vipGuest);
            }

            foreach (var regularGuest in regularGuests)
            {
                Console.WriteLine(regularGuest);
            }

        }
    }
}
