using AnimalCentre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var animalCentre = new AnimalCentre();

            var input = Console.ReadLine();
            while (input != "End")
            {
                var tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var command = tokens[0];
                var result = "";
                try
                {
                    switch (command)
                    {
                        case "RegisterAnimal":
                            result = animalCentre.RegisterAnimal(tokens[1], tokens[2], int.Parse(tokens[3]), int.Parse(tokens[4]), int.Parse(tokens[5]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Chip":
                            result = animalCentre.Chip(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Vaccinate":
                            result = animalCentre.Vaccinate(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Fitness":
                            result = animalCentre.Fitness(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Play":
                            result = animalCentre.Play(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "DentalCare":
                            result = animalCentre.DentalCare(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "NailTrim":
                            result = animalCentre.NailTrim(tokens[1], int.Parse(tokens[2]));
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "Adopt":
                            result = animalCentre.Adopt(tokens[1], tokens[2]);
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                        case "History":
                            result = animalCentre.History(tokens[1]);
                            if (result != "")
                            {
                                Console.WriteLine(result);
                            }
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("ArgumentException: " + ae.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine("InvalidOperationException: " + ioe.Message);
                }

                input = Console.ReadLine();
            }

            var resultString = animalCentre.GetAdoptedAnimals();

            if (resultString != "")
            {
                Console.WriteLine(resultString);
            }
        }
    }
}
