using System;
using System.Windows;
using Hotel.classes;
using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void CloseBut_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult askExit = MessageBox.Show("Вы хотите выйти из приложения \"Гостиница\"?", "ВЫХОД ИЗ ПРИЛОЖЕНИЯ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (askExit == MessageBoxResult.Yes)
            {
                BackUpSql.CreateBackUp(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                Application.Current.Shutdown();
            }
        }

        private void LogInBut_OnClick(object sender, RoutedEventArgs e)
        {
            if(Login.Text == "" && (Passwd.Password == "" || RepeatPasswd.Password == ""))
                MessageBox.Show("Введите логин и пароль!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if(Login.Text == "")
                MessageBox.Show("Введите логин!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            else if(Login.Text != "" && Passwd.Password == "" || RepeatPasswd.Password == "")
                MessageBox.Show("Введите пароль!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if(Passwd.Password != RepeatPasswd.Password)
                    MessageBox.Show("Пароль не совпадает!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    string login = Login.Text;
                    string passwd = GetHashPasswd.GetHash(Passwd.Password);
                    
                    string logInQuery = string.Format("SELECT * FROM hotel.user WHERE u_login = '{0}' AND u_password = '{1}';", login, passwd);
                    try
                    {
                        classes.MySql.Connection.Open();

                        MySqlCommand command = new MySqlCommand(logInQuery, classes.MySql.Connection);
                        var checkUser = command.ExecuteScalar();
                        if (checkUser == null)
                            MessageBox.Show("Неверно введен логин или пароль!", "ОШИБКА АВТОРИЗАЦИИ", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            MySqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                WhichUserType.UserType = reader.GetString(4);
                            }
                            MessageBox.Show("Вы успешно авторизовались.\nДобро пожаловать!", "УСПЕШНАЯ АВТОРИЗАЦИЯ", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                            NavigationService.Navigate(new MainPage());
                        }
                        classes.MySql.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
