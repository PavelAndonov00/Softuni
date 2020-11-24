using DB_Apps_Introduction;
using System;
using System.Data.SqlClient;

namespace _03.Minions_Name
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string nameQuery = $"SELECT Name FROM Villains WHERE Id = {villainId}";
                SqlCommand command = new SqlCommand(nameQuery, connection);

                using (command)
                {
                    string villainName = (string)command.ExecuteScalar();
                    if(villainName == null)
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"Villain: {villainName}");

                        string minionsQuery = $"SELECT m.Name, m.Age, ROW_NUMBER() OVER(ORDER BY m.Name) AS RowNum FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE VillainId = {villainId} ORDER BY m.Name";

                        command = new SqlCommand(minionsQuery, connection);
                        SqlDataReader dataReader = command.ExecuteReader();
                        using (dataReader)
                        {
                            if (!dataReader.HasRows)
                            {
                                Console.WriteLine($"Villain: {villainName}(no minions)");
                            }
                            else
                            {
                                while (dataReader.Read())
                                {
                                    Console.WriteLine($"{dataReader["RowNum"]}. {dataReader["Name"]} {dataReader["Age"]}");
                                }
                            }

                        }

                    }

                }

                connection.Close();
            }
        }
    }
}
