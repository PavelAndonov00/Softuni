namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            Regex validPattern = new Regex(@"^[d-z{}|# ]+$");
            string stringToDecrypt = Console.ReadLine();
            if (!validPattern.IsMatch(stringToDecrypt))
            {
                Console.WriteLine("This is not the book you are looking for.");
                return;
            }

            for (int i = 0; i < stringToDecrypt.Length; i++)
            {
                char ch = stringToDecrypt[i];
                ch -= (char)3;

                stringToDecrypt = stringToDecrypt.Substring(0, i) + ch + stringToDecrypt.Substring(i + 1);
            }

            string[] replacements = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);
            stringToDecrypt = stringToDecrypt.Replace(replacements[0], replacements[1]);
            Console.WriteLine(stringToDecrypt);
        }
    }
}
