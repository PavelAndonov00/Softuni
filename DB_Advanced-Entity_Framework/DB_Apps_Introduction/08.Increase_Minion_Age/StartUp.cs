using DB_Apps_Introduction;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace _08.Increase_Minion_Age
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionIds = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                foreach (var minionId in minionIds)
                {
                    string updateMinionAge = $"UPDATE Minions SET Age += 1 WHERE Id = {minionId}";
                    SqlCommand command = new SqlCommand(updateMinionAge, connection);
                    using (command)
                    {
                        command.ExecuteNonQuery();

                        string getMinionsAndAge = $"SELECT Name, Age FROM Minions WHERE Id = {minionId}";
                        command = new SqlCommand(getMinionsAndAge, connection);

                        SqlDataReader dataReader = command.ExecuteReader();
                        using (dataReader)
                        {
                            dataReader.Read();
                            string name = dataReader["Name"].ToString().Trim();
                            name = name[0].ToString().ToUpper() + name.Substring(1);
                            int age = (int)dataReader["Age"];

                            Console.WriteLine(name + " " + age);
                        }
                    }

                }

                connection.Close();
            }
        }
    }
}
