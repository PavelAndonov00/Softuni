namespace p11_Threeuple
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] firstLine = Console.ReadLine().Split();
            string fullName = firstLine[0] + " " + firstLine[1];
            string address = firstLine[2];
            string town = firstLine[3];
            Threeuple<string, string, string> firstTuple = new Threeuple<string, string, string>(fullName, address, town);
            Console.WriteLine(firstTuple);

            string[] secondLine = Console.ReadLine().Split();
            string drinkerName = secondLine[0];
            int litersOfBeer = int.Parse(secondLine[1]);
            bool drunkOrNot = secondLine[2] == "drunk" ? true : false;
            Threeuple<string, int, bool> secondTuple = new Threeuple<string, int, bool>(drinkerName, litersOfBeer,drunkOrNot);
            Console.WriteLine(secondTuple);

            string[] thirdLine = Console.ReadLine().Split();
            string customerName = thirdLine[0];
            double accountBalance = double.Parse(thirdLine[1]);
            string bankName = thirdLine[2];
            Threeuple<string, double, string> thirdTuple = new Threeuple<string, double, string>(customerName, accountBalance, bankName);
            Console.WriteLine(thirdTuple);
        }
    }
}
