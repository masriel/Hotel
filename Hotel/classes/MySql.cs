using System;
using System.Windows;

using MySql.Data.MySqlClient;

namespace Hotel.classes
{
    public abstract class MySql
    {
        public const string ConStr = "Server=localhost;Port=3306;Database=hotel;Uid=root;Pwd=;";
        public static readonly MySqlConnection Connection = new MySqlConnection(ConStr);
        public static void AddCommand(string query)
        {
            try
            {
                using (Connection)
                {
                    Connection.Open();

                    MySqlCommand command = new MySqlCommand(query, Connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
