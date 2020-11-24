using DB_Apps_Introduction;
using System;
using System.Data.SqlClient;

namespace _04.Add_Minion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] tokens = Console.ReadLine().Split();
            string minionName = tokens[1];
            int minionAge = int.Parse(tokens[2]);
            string minionTown = tokens[3];

            tokens = Console.ReadLine().Split();
            string villainName = tokens[1];

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string minionTownQuery = $"SELECT Name FROM Towns WHERE Name = '{minionTown}'";
                SqlCommand command = new SqlCommand(minionTownQuery, connection);
                using (command)
                {
                    var result = command.ExecuteScalar();
                    if(result == null)
                    {
                        string insertTownQuery = $"INSERT INTO Towns(Name) VALUES ('{minionTown}')";
                        command = new SqlCommand(insertTownQuery, connection);
                        command.ExecuteNonQuery();

                        Console.WriteLine($"Town {minionTown} was added to the database");
                    }

                    string villainNameQuery = $"SELECT Name FROM Villains WHERE Name = '{villainName}'";
                    command = new SqlCommand(villainNameQuery, connection);
                    result = command.ExecuteScalar();
                    if(result == null)
                    {
                        string insertVillainQuery = $"INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('{villainName}', 4)";
                        command = new SqlCommand(insertVillainQuery, connection);
                        command.ExecuteNonQuery();

                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }

                    string getMinionIdQuery = $"SELECT Id FROM Minions WHERE Name = '{minionName}'";
                    command = new SqlCommand(getMinionIdQuery, connection);
                    int minionId = (int)command.ExecuteScalar();

                    string getVillainIdQuery = $"SELECT Id FROM Villains WHERE Name = '{villainName}'";
                    command = new SqlCommand(getVillainIdQuery, connection);
                    int villainId = (int)command.ExecuteScalar();

                    string insertIntoMinionsVillainsQuery = $"INSERT INTO MinionsVillains VALUES ({minionId}, {villainId})";
                    command = new SqlCommand(insertIntoMinionsVillainsQuery, connection);
                    command.ExecuteNonQuery();

                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }

                connection.Close();
            }
        }
    }
}
