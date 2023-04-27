using System.Windows;
using System.Windows.Input;
using Hotel.classes;

namespace Hotel.pages
{
    public partial class AddUser
    {
        public AddUser()
        {
            InitializeComponent();
        }
        
        private void CloseBut_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите вернуться в главное меню?", "ВЫХОД", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        private void UsernameBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!(char.IsLetter(e.Text, 0) || (e.Text == ".")))
                e.Handled = true;
        }

        private void LoginBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "'")
                e.Handled = true;
        }

        private void GeneratePwd_OnClick(object sender, RoutedEventArgs e)
        {
            PasswdBox.Password = GenerateRwd.GeneratePasswd();
        }

        private void SignInBut_OnClick(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string login = LoginBox.Text;
            string passwd = GetHashPasswd.GetHash(PasswdBox.Password);

            if(username == string.Empty || login == string.Empty || passwd == string.Empty) 
            {
                MessageBox.Show("Все поля должны быть заполнены!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(passwd.Length < 8)
            {
                MessageBox.Show("Пароль должен быть не меньше 8 символов!", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string queryadd = $"INSERT INTO user (u_name, u_login, u_password, u_type) VALUES ('{username}','{login}','{passwd}','user');";

                MessageBoxResult boxResult = MessageBox.Show($"Вы действительно хотите добавить пользователя {login} ({username})?", "ДОБАВЛЕНИЕ ПОЛЬЗОВАТЕЛЯ", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(boxResult == MessageBoxResult.Yes)
                {
                    classes.MySql.AddCommand(queryadd);
                    UsernameBox.Clear();
                    LoginBox.Clear();
                    PasswdBox.Clear();
                }
            }
        }

        private void UserDBBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserDataBase());
        }
    }
}