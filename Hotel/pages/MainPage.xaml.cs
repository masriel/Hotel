using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Hotel.classes;
using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void CLoseBut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult AskExit = MessageBox.Show("Вы хотите выйти из приложения \"Гостиница\"?", "ВЫХОД ИЗ ПРИЛОЖЕНИЯ", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (AskExit == MessageBoxResult.Yes)
            {
                BackUpSql.CreateBackUp(DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss"));
                Application.Current.Shutdown();
            }
        }

        private void LogOutBut_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Хотите выйти из аккаунта?", "ВЫХОД ИЗ АККАУНТА", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new Authorization());
            }
        }

        private void AdminPanel_Click(object sender, RoutedEventArgs e)
        {
            Panel.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            classes.MySql.Connection.Close();

            ArrivalDate.SelectedDate = DateTime.Now;
            DepartureDate.SelectedDate = DateTime.Now.AddDays(1);
            
            if (WhichUserType.UserType == "user")
            {
                AddUserBut.Visibility = Visibility.Hidden;
                AdminPanel.Visibility = Visibility.Hidden;
                Text_.Text = "АИС Гостиница";
            }

            LoadData("SELECT * FROM hotel.room;");
        }

        public void LoadData(string query)
        {
            try
            {
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection))
                    {
                        List<Rooms> rooms = new List<Rooms>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rooms.Add(new Rooms { Name = reader.GetString(1), Area = Convert.ToInt32(reader.GetString(2)), VisCount = Convert.ToInt32(reader.GetString(3)), Cost = Convert.ToInt32(reader.GetString(4)), Availble = Convert.ToInt32(reader.GetString(5)), Image = String.Format(Directory.GetCurrentDirectory() + "\\images\\{0}", reader.GetString(6)) });
                            }
                            RoomsList.ItemsSource = rooms;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData(GetQuery());
        }

        public string GetQuery()
        {
            string sql = String.Empty;

            if (SearchBox.Text != "") sql = String.Format("SELECT * FROM hotel.room WHERE r_name LIKE '%{0}%'", SearchBox.Text);
            else sql = "SELECT * FROM hotel.room";

            switch (FilterBox.SelectedIndex)
            {
                case 0:
                    sql += " ORDER BY cost";
                    break;
                case 1:
                    sql += " ORDER BY visitors_quant";
                    break;
                case 2:
                    sql += " ORDER BY available_quant";
                    break;
                default:
                    sql += " ORDER BY cost";
                    break;
            }

            switch (SortBox.SelectedIndex)
            {
                case 0:
                    sql += " DESC";
                    break;
                case 1:
                    sql += " ASC";
                    break;
            }

            return sql += ";";
        }

        private void _SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData(GetQuery());
        }
        
        private void AddUserBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUser());
        }

        private void ClientBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientPage());
        }

        private void But_OnClick(object sender, RoutedEventArgs e)
        {
            if (ArrivalDate.SelectedDate > DepartureDate.SelectedDate)
                MessageBox.Show("Некорректная дата заезда", "ОШИБКА ЗАПОЛНЕНИЯ", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (NumberOfVisit.SelectedItem != null)
                {
                    ForBooking.DataArriv = ArrivalDate.SelectedDate.ToString();
                    ForBooking.DataDepart = DepartureDate.SelectedDate.ToString();
                    ForBooking.NumVis = int.Parse(NumberOfVisit.Text);
                    NavigationService.Navigate(new Booking());
                }
                else
                    MessageBox.Show("Выберите количество посетителей", "ОШИБКА ЗАПОЛНЕНИЯ", 
                        MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void RoomBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomPage());
        }

        private void ArrivalDate_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArrivalDate.SelectedDate > DateTime.Now) But.Content = "Бронировать";
            else But.Content = "Оформить";
        }

        private void DepartureDate_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartureDate.SelectedDate < DateTime.Now) DepartureDate.SelectedDate = DateTime.Now.AddDays(1);
        }

        private void ViewBookBut_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AllBookingPage());
        }
    }
}
