using DB_Apps_Introduction;
using System;
using System.Data.SqlClient;

namespace _02.Villain_Names
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(Configuration.connectionString);

            using (connection)
            {
                connection.Open();

                string sqlQuery = @"SELECT v.Name, COUNT(mv.MinionId) FROM MinionsVillains AS mv JOIN Villains AS v ON v.Id = mv.VillainId
GROUP BY v.EvilnessFactorId, v.Name HAVING COUNT(mv.MinionId) > 3 ORDER BY COUNT(mv.MinionId) DESC";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader dataReader = command.ExecuteReader();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine(dataReader[0] + " -> " + dataReader[1]);
                    }
                }

                connection.Close();
            }
        }
    }
}
