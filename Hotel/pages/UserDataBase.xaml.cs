using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Hotel.classes;
using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    public partial class UserDataBase
    {
        private readonly DataTable _table = new DataTable();
        private string _pwd = string.Empty;
        private int _id;
        
        public UserDataBase()
        {
            InitializeComponent();
        }

        private void LoadDb()
        {
            try
            {
                _table.Clear();
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand command = new MySqlCommand("SELECT u_name AS 'NAME', u_login AS 'LOGIN', u_password AS 'PASSWORD', u_type AS 'TYPE' FROM hotel.user;", classes.MySql.Connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(_table);
                    DataUser.ItemsSource = _table.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ReturnBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser());
        }

        private void CloseBut_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите вернуться в главное меню?", "ВЫХОД", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        private void DataUser_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var login = _table.DefaultView[DataUser.SelectedIndex]["LOGIN"];

            if (login == null)
            {
                MessageBox.Show("Строка не выбрана", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string query = $"SELECT IDuser, u_name, u_login, u_password FROM hotel.user WHERE hotel.user.u_login = '{login}';";

                using(classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        _id = reader.GetInt32(0);
                        UsernameBox.Text = reader.GetString(1);
                        LoginBox.Text = reader.GetString(2);
                        _pwd = reader.GetString(3);
                    }
                }
            }
        }

        private void GeneratePwd_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PasswdBox.Password = GenerateRwd.GeneratePasswd();
        }

        private void DeleteBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить пользователя\n{UsernameBox.Text}?", "УДАЛЕНИЕ", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        MySqlCommand command = new MySqlCommand($"DELETE FROM hotel.user WHERE u_login = '{LoginBox.Text}';", classes.MySql.Connection);
                        command.ExecuteNonQuery();
                        //classes.MySql.connection.Close();
                    }
                }
                LoadDb();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            UsernameBox.Clear();
            LoginBox.Clear();
            PasswdBox.Clear();
        }

        private void DoneBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (UsernameBox.Text == String.Empty || LoginBox.Text == String.Empty)
                MessageBox.Show("Все поля должны быть заполнены!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if (PasswdBox.Password != "" && PasswdBox.Password != " ")
            {
                if (PasswdBox.Password.Length >= 8)
                    _pwd = PasswdBox.Password;
                else
                    MessageBox.Show("Пароль должен быть не меньше восьми символов!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    using (classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        string query = $"UPDATE hotel.user SET u_name = '{UsernameBox.Text}', u_login = '{LoginBox.Text}', u_password = '{_pwd}' WHERE IDuser = {_id}";
                        MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                        command.ExecuteNonQuery();
                    }

                    LoadDb();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                UsernameBox.Clear();
                LoginBox.Clear();
                PasswdBox.Clear();
            }
        }

        private void UserDataBase_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadDb();
        }

        private void ExpBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using(classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand comand = new MySqlCommand("SELECT * FROM hotel.user;", classes.MySql.Connection);
                    MySqlDataReader reader = comand.ExecuteReader();

                    DataTable userTable = new DataTable();
                    userTable.Load(reader);

                    using (StreamWriter writer = new StreamWriter($@"{Directory.GetCurrentDirectory()}\\ExportCsv\\UserData.csv", false))
                    {
                        for(int columnCount = 0; columnCount <= userTable.Columns.Count - 1; ++columnCount)
                        {
                            if (columnCount != userTable.Columns.Count - 1) writer.Write(userTable.Columns[columnCount].ColumnName + ";");
                            else writer.Write(userTable.Columns[columnCount].ColumnName);
                        }
                        writer.Write("\n");

                        foreach(DataRow row in userTable.Rows)
                        {
                            string str = String.Join(";", row.ItemArray);
                            writer.WriteLine(str);
                        }
                    }
                }
                MessageBox.Show("Данные о пользователях системы выгружены!", "ЭКСПОРТ ДАННЫХ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }
    }
}