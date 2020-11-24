using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StorageMaster
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var master = new StorageMaster();

            var input = Console.ReadLine();
            while (input != "END")
            {
                var tokens = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var type = tokens[0];
                try
                {
                    switch (type)
                    {
                        case "AddProduct":
                            Console.WriteLine(master.AddProduct(tokens[1], double.Parse(tokens[2])));
                            break;
                        case "RegisterStorage":
                            Console.WriteLine(master.RegisterStorage(tokens[1], tokens[2]));
                            break;
                        case "SelectVehicle":
                            Console.WriteLine(master.SelectVehicle(tokens[1], int.Parse(tokens[2])));
                            break;
                        case "LoadVehicle":                           
                            Console.WriteLine(master.LoadVehicle(tokens.Skip(1)));
                            break;
                        case "SendVehicleTo":
                            Console.WriteLine(master.SendVehicleTo(tokens[1], int.Parse(tokens[2]), tokens[3]));
                            break;
                        case "UnloadVehicle":
                            Console.WriteLine(master.UnloadVehicle(tokens[1], int.Parse(tokens[2])));
                            break;
                        case "GetStorageStatus":
                            Console.WriteLine(master.GetStorageStatus(tokens[1]));
                            break;
                    }
                }
                catch(InvalidOperationException ioe)
                {
                    Console.WriteLine("Error: " + ioe.Message);
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(master.GetSummary());

        }
    }
}

