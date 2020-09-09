using System;
using System.Data.SqlClient;

namespace AAAA
{
    class Program
    {
        private static string connectionString = "Data Source=DESKTOP-604RL6A;Initial Catalog=ZooManager;Integrated Security=True";
        private static SqlConnection sqlConnection;
        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            ShowInfo();
            sqlConnection.Close();
            Console.ReadKey();
        }

        private static void AddInfo(string name, string color)
        {
            string query = "INSERT INTO Animals(name, color) VALUES(@name, @color)";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            using (command)
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@color", color);
                command.ExecuteNonQuery();
            }
        }

        private static void Delete(int id)
        {
            string query = "DELETE FROM Animals WHERE id=@id";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            using (command)
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void ShowInfo()
        {
            using (SqlCommand command = new SqlCommand(
                "SELECT * FROM Animals",
                sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("ID: " + reader.GetInt32(2).ToString());
                        Console.WriteLine("Name: " + reader.GetString(0));
                        Console.WriteLine("Color: " + reader.GetString(1));
                        Console.WriteLine("------------------------------");
                        //for(int i = 0; i < reader.FieldCount; i++)
                        //{
                        //    Console.WriteLine(reader.GetString(i));
                        //}

                    }
                }
            }
        }
    }
}
