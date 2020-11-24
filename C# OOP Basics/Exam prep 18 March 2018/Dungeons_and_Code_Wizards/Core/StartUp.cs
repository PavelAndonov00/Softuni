using System;
using System.Linq;

namespace DungeonsAndCodeWizards
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var master = new DungeonMaster();

            var input = Console.ReadLine();
            while (!String.IsNullOrEmpty(input))
            {
                try
                {
                    var arguments = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var prefix = arguments[0];
                    var info = arguments.Skip(1).ToArray();
                    switch (prefix)
                    {
                        case "JoinParty":
                            Console.WriteLine(master.JoinParty(info));
                            break;
                        case "AddItemToPool":
                            Console.WriteLine(master.AddItemToPool(info));
                            break;
                        case "PickUpItem":
                            Console.WriteLine(master.PickUpItem(info));
                            break;
                        case "UseItem":
                            Console.WriteLine(master.UseItem(info));
                            break;
                        case "UseItemOn":
                            Console.WriteLine(master.UseItemOn(info));
                            break;
                        case "GiveCharacterItem":
                            Console.WriteLine(master.GiveCharacterItem(info));
                            break;
                        case "GetStats":
                            Console.WriteLine(master.GetStats());
                            break;
                        case "Attack":
                            Console.WriteLine(master.Attack(info));
                            break;
                        case "Heal":
                            Console.WriteLine(master.Heal(info));
                            break;
                        case "EndTurn":
                            Console.WriteLine(master.EndTurn(info));
                            break;
                        case "IsGameOver":
                            Console.WriteLine(master.IsGameOver());
                            break;
                    }
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine($"Parameter Error: {ae.Message}");
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine($"Invalid Operation: {ioe.Message}");
                }

                if (master.IsGameOver())
                {
                    break;
                }

                input = Console.ReadLine();
            }

            Console.WriteLine($"Final stats:");
            Console.WriteLine(master.GetStats());
        }

    }
}