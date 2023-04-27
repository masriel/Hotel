using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Hotel.classes;

using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    public partial class AllBookingPage
    {
        
        private int _curPage = 1, _allPages = 0, _limit = 2, _allLines = 0, _startLine = 0;
        public AllBookingPage()
        {
            InitializeComponent();
        }

        private void AllBookingPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            string q = $"SELECT reg_num, arrival_date, departure_date, (select surname from hotel.client where hotel.client.IDclient = booking.IDclient) as resident, (select r_name from hotel.room where room.NUMroom = booking.NUMroom) as room, (select area from hotel.room where room.NUMroom = booking.NUMroom) as area,(select cost from hotel.room where room.NUMroom = booking.NUMroom) as cost, count_day, (select f_name from hotel.feed where feed.IDfeed = booking.IDfeed) as feed, amount FROM hotel.booking LIMIT {_limit} OFFSET {_startLine};";
            LoadData(q);

            _allLines = CountRow();
            _allPages = (int)Math.Ceiling((double)_allLines / (double)_limit);
            AllPage.Text = _allPages.ToString();
        }

        private void LoadData(string query)
        {
            try
            {
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection))
                    {
                        List<Reservation> reservation = new List<Reservation>();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservation.Add(new Reservation { RegNum = reader.GetString(0),
                                    ArrivalDate = reader.GetString(1).Substring(0, 10), 
                                    DepartureDate = reader.GetString(2).Substring(0, 10),
                                    Client = reader.GetString(3), RoomName = reader.GetString(4),
                                    RoomArea = reader.GetString(5), RoomCost = reader.GetString(6),
                                    Feed = reader.GetString(8), Amount = reader.GetString(9)
                                });
                            }

                            ReservationList.ItemsSource = reservation;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int CountRow()
        {
            int lines = 0;
            using (classes.MySql.Connection)
            {
                DataTable dt = new DataTable();
                classes.MySql.Connection.Open();
                using (MySqlCommand command =
                       new MySqlCommand("SELECT * FROM hotel.booking", classes.MySql.Connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        lines = dt.Rows.Count;
                    }
                }
            }
            return lines;
        }

        private void CloseBut_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите вернуться на главное меню?", "ВЫХОД", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        private void PreviousPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (_curPage > 1)
            {
                _curPage -= 1;
                CurrentPage.Text = _curPage.ToString();
                _startLine -= 2;
                string q =
                    $"SELECT reg_num, arrival_date, departure_date, (select surname from hotel.client where hotel.client.IDclient = booking.IDclient) as resident, (select r_name from hotel.room where room.NUMroom = booking.NUMroom) as room, (select area from hotel.room where room.NUMroom = booking.NUMroom) as area,(select cost from hotel.room where room.NUMroom = booking.NUMroom) as cost, count_day, (select f_name from hotel.feed where feed.IDfeed = booking.IDfeed) as feed, amount FROM hotel.booking LIMIT {_limit} OFFSET {_startLine};";
                LoadData(q);
            }
        }

        private void NextPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (_curPage < _allPages)
            {
                _curPage += 1;
                CurrentPage.Text = _curPage.ToString();
                _startLine += 2;
                string q =
                    $"SELECT reg_num, arrival_date, departure_date, (select surname from hotel.client where hotel.client.IDclient = booking.IDclient) as resident, (select r_name from hotel.room where room.NUMroom = booking.NUMroom) as room, (select area from hotel.room where room.NUMroom = booking.NUMroom) as area,(select cost from hotel.room where room.NUMroom = booking.NUMroom) as cost, count_day, (select f_name from hotel.feed where feed.IDfeed = booking.IDfeed) as feed, amount FROM hotel.booking LIMIT {_limit} OFFSET {_startLine};";
                LoadData(q);
            }
        }
    }
}