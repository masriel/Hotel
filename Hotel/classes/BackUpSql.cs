using System;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotel.classes
{
    public class BackUpSql
    {
        public static void CreateBackUp(string dateNow)
        {
            string file = $"{Directory.GetCurrentDirectory()}\\BackUp\\{dateNow}.sql";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(MySql.ConStr))
                {
                    using (var command = new MySqlCommand())
                    {
                        using (var backUp = new MySqlBackup(command))
                        {
                            command.Connection = connection;
                            connection.Open();
                            backUp.ExportToFile(file);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}