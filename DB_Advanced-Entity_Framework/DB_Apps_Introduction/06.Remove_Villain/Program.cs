using DB_Apps_Introduction;
using System;
using System.Data.SqlClient;

namespace _06.Remove_Villain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);

            using (connection)
            {
                connection.Open();

                SqlCommand command = new SqlCommand($"SELECT Name FROM Villains WHERE Id = {villainId}", connection);
                using (command)
                {
                    string villainName = (string)command.ExecuteScalar();
                    if(villainName == null)
                    {
                        Console.WriteLine("No such villain was found.");
                    }
                    else
                    {
                        command = new SqlCommand($"DELETE MinionsVillains WHERE VillainId = {villainId}", connection);
                        command.ExecuteNonQuery();
                        command = new SqlCommand($"SELECT @@ROWCOUNT", connection);
                        int minionsCount = (int)command.ExecuteScalar();

                        Console.WriteLine(villainName + " was deleted.");
                        Console.WriteLine(minionsCount + " minions were released.");
                    }
                }

                connection.Close();
            }
        }
    }
}
