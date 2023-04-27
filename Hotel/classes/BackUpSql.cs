using System;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Hotel.classes
{
    public class BackUpSql
    {
        public static void CreateBackUp(string DateNow)
        {
            string _file = $"{Directory.GetCurrentDirectory()}\\BackUp\\{DateNow}.sql";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(classes.MySql.ConStr))
                {
                    using (var command = new MySqlCommand())
                    {
                        using (var _backUp = new MySqlBackup(command))
                        {
                            command.Connection = connection;
                            connection.Open();
                            _backUp.ExportToFile(_file);
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