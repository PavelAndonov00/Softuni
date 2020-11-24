namespace p08_Pet_Clinic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Clinic> clinics = new List<Clinic>();
            List<Pet> pets = new List<Pet>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string command = tokens[0];
                tokens = tokens.Skip(1).ToArray();
                try
                {
                    switch (command)
                    {
                        case "Create":
                            if (tokens[0] == "Pet")
                            {
                                pets.Add(new Pet(tokens[1], int.Parse(tokens[2]), tokens[3]));
                            }
                            else
                            {
                                clinics.Add(new Clinic(tokens[1], int.Parse(tokens[2])));
                            }
                            break;
                        case "Add":
                            Console.WriteLine(clinics.FirstOrDefault(c => c.Name == tokens[1]).Add(pets.FirstOrDefault(p => p.Name == tokens[0])));
                            break;
                        case "Release":
                            Console.WriteLine(clinics.FirstOrDefault(c => c.Name == tokens[0]).Release());
                            break;
                        case "HasEmptyRooms":
                            Console.WriteLine(clinics.FirstOrDefault(c => c.Name == tokens[0]).HasEmptyRooms());
                            break;
                        case "Print":
                            if (tokens.Length == 1)
                            {
                                Console.WriteLine(clinics.FirstOrDefault(c => c.Name == tokens[0]).Print());
                            }
                            else
                            {
                                Console.WriteLine(clinics.FirstOrDefault(c => c.Name == tokens[0]).Print(int.Parse(tokens[1])));
                            }
                            break;
                    }
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);                    
                }
            }
        }
    }
}
