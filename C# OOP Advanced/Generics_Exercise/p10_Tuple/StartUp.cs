namespace p10_Tuple
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] firstLine = Console.ReadLine().Split();
            string fullName = firstLine[0] + " " + firstLine[1];
            string address = firstLine[2];
            TupleClass<string, string> firstTuple = new TupleClass<string, string>(fullName, address);
            Console.WriteLine(firstTuple);

            string[] secondLine = Console.ReadLine().Split();
            string name = secondLine[0];
            int litersOfBeer = int.Parse(secondLine[1]);
            TupleClass<string, int> secondTuple = new TupleClass<string, int>(name, litersOfBeer);
            Console.WriteLine(secondTuple);

            string[] thirdLine = Console.ReadLine().Split();
            int integerNumber = int.Parse(thirdLine[0]);
            double doubleNumber = double.Parse(thirdLine[1]);
            TupleClass<int, double> thirdTuple = new TupleClass<int, double>(integerNumber, doubleNumber);
            Console.WriteLine(thirdTuple);
        }
    }
}