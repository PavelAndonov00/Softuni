using DB_Apps_Introduction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _05.Change_Towns_Name_Casing
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string countryName = Console.ReadLine();

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string getCountryIdQuery = $"SELECT Id FROM Countries WHERE Name = '{countryName}'";
                SqlCommand command = new SqlCommand(getCountryIdQuery, connection);
                using (command)
                {
                    var countryId = command.ExecuteScalar();
                    if(countryId == null)
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                    else
                    {
                        string getTownsQuery = $"SELECT Id, Name FROM Towns WHERE CountryCode = {(int)countryId}";
                        command = new SqlCommand(getTownsQuery, connection);
                        SqlDataReader dataReader = command.ExecuteReader();
                        using (dataReader)
                        {
                            if (!dataReader.HasRows)
                            {
                                Console.WriteLine("No town names were affected.");
                            }
                            else
                            {
                                Dictionary<string, int> towns = new Dictionary<string, int>();
                                while (dataReader.Read())
                                {
                                    int townId = (int)dataReader["Id"];
                                    string townName = dataReader["Name"].ToString().ToUpper();

                                    towns[townName] = townId;
                                }

                                dataReader.Close();
                                foreach (var kv in towns)
                                {
                                    string updateQuery = $"UPDATE Towns SET Name = '{kv.Key}' WHERE Id = {kv.Value}";
                                    var newCommand = new SqlCommand(updateQuery, connection);
                                    newCommand.ExecuteNonQuery();
                                }

                                Console.WriteLine($"{towns.Count} town names were affected.");
                                Console.WriteLine($"[{String.Join(", ", towns.Select(e => e.Key))}]");
                            }                  
                        }
                    }
                }

                connection.Close();
            }
        }
    }
}
