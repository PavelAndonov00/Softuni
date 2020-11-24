using DB_Apps_Introduction;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace _09.Increase_Age_Stored_Procedure
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int minionId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(Configuration.connectionString);
            using (connection)
            {
                connection.Open();

                string execProcedureGetOlder = $"EXEC usp_GetOlder @Id = {minionId}";
                SqlCommand command = new SqlCommand(execProcedureGetOlder, connection);
                using (command)
                {
                    command.ExecuteNonQuery();

                    string getNameAndAge = $"SELECT Name, Age FROM Minions WHERE Id = {minionId}";
                    command = new SqlCommand(getNameAndAge, connection);

                    SqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();

                    string minionName = (string)dataReader["Name"];
                    int minionAge = (int)dataReader["Age"];

                    Console.WriteLine(minionName + " - " + minionAge + " years old");
                    
                }

                connection.Close();
            }

        }
    }
}
