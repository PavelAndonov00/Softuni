using DB_Apps_Introduction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _07.Print_All_Minion_Names
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.connectionString);

            using (connection)
            {
                connection.Open();

                string minionNamesQuery = "SELECT Name FROM Minions";
                SqlCommand command = new SqlCommand(minionNamesQuery, connection);

                using (command)
                {
                    SqlDataReader reader = command.ExecuteReader();
                    using (reader)
                    {
                        List<string> minions = new List<string>();
                        while (reader.Read())
                        {
                            minions.Add((string)reader["Name"]);
                        }                  
                        
                        int firstPartCount = (int)Math.Ceiling(minions.Count / 2.0);
                        List<string> firstPart = minions.Take(firstPartCount).ToList();

                        List<string> secondPart = minions.Skip(firstPartCount).Reverse().ToList();

                        Console.WriteLine("Original Order:");
                        foreach (var minion in minions)
                        {
                            Console.WriteLine(minion);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Output:");
                        for (int i = 0; i < minions.Count / 2; i++)
                        {
                            Console.WriteLine(minions[i]);
                            Console.WriteLine(minions[minions.Count - 1 - i]);
                        }

                        if (minions.Count % 2 != 0)
                        {
                            Console.WriteLine(minions[minions.Count / 2]);
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
