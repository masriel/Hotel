using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotel.classes;
using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    public partial class ClientPage
    {
        private readonly DataTable _table = new DataTable();
        private int _id;
        private bool _checkedStr;
        
        public ClientPage()
        {
            InitializeComponent();
        }

        private void LoadDb(string query)
        {
            try
            {
                _table.Clear();
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(_table);
                    ClientDG.ItemsSource = _table.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            LoadDb(String.Format("SELECT surname AS 'Фамилия', name AS 'Имя', patronymic AS 'Отчество', SUBSTRING(birth_date, 1, 10) AS 'ДатаРождения', age AS 'Возраст', phone_number AS 'Телефон', passport_series AS 'Серия', passport_number AS 'Номер', SUBSTRING(date_of_issue, 1, 10) AS 'ДатаВыдачи' FROM hotel.client WHERE surname LIKE '%{0}%';", SearchBox.Text));
        }

        private void ReturnBut_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите закрыть панель управления клиентами?", "ВЫХОД", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        private void ClientDG_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _checkedStr = true;

            var surname = _table.DefaultView[ClientDG.SelectedIndex]["Фамилия"];

            if (surname != null)
            {
                try
                {
                    using (classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        MySqlCommand command = new MySqlCommand(String.Format("SELECT * FROM hotel.client WHERE client.surname = '{0}';", surname), classes.MySql.Connection);
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            _id = Convert.ToInt32(reader.GetString(0));
                            SurnameBox.Text = reader.GetString(1);
                            NameBox.Text = reader.GetString(2);
                            PatrBox.Text = reader.GetString(3);
                            string tmps = reader.GetString(4);
                            string[] tmp = tmps.Split(' ');
                            BirthDateBox.Text = tmp[0];
                            PhoneBox.Text = reader.GetString(6);
                            PasSeries.Text = reader.GetString(7);
                            PasNumber.Text = reader.GetString(8);
                            tmps = reader.GetString(9);
                            tmp = tmps.Split(' ');
                            DateIssue.Text = tmp[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SurnameBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsLetter(e.Text, 0)))
                e.Handled = true;
        }

        private void PasSeries_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (PasSeries.Text.Length >= 4 || !(Char.IsDigit(e.Text, 0)))
                e.Handled = true;
        }

        private void PasNumber_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (PasNumber.Text.Length >= 6 || !(Char.IsDigit(e.Text, 0)))
                e.Handled = true;
        }

        private void DateIssue_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((DateIssue.Text.Length >= 10 || !(Char.IsDigit(e.Text, 0))) && (BirthDateBox.Text.Length >= 10 || !(Char.IsDigit(e.Text, 0))))
                e.Handled = true;
        }

        private void PhoneBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (PhoneBox.Text.Length >= 11 || !(Char.IsDigit(e.Text, 0)))
                e.Handled = true;
        }

        private void DeleteBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_checkedStr == false)
                MessageBox.Show("Выберите клиента которого хотите удалить из базы", "УДАЛЕНИЕ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    using (classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить клиента?", "УДАЛЕНИЕ КЛИЕНТА", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            MySqlCommand command = new MySqlCommand(String.Format("DELETE FROM hotel.client WHERE IDclient = {0};", _id), classes.MySql.Connection);
                            command.ExecuteNonQuery();

                            SurnameBox.Clear();
                            NameBox.Clear();
                            PatrBox.Clear();
                            BirthDateBox.Clear();
                            PhoneBox.Clear();
                            PasSeries.Clear();
                            PasNumber.Clear();
                            DateIssue.Clear();
                        }
                    }
                    LoadDb("SELECT surname AS 'Фамилия', name AS 'Имя', patronymic AS 'Отчество', SUBSTRING(birth_date, 1, 10) AS 'ДатаРождения', age AS 'Возраст', phone_number AS 'Телефон', passport_series AS 'Серия', passport_number AS 'Номер', SUBSTRING(date_of_issue, 1, 10) AS 'ДатаВыдачи' FROM hotel.client;");

                    _checkedStr = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DoneBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_checkedStr == false)
                MessageBox.Show("Выберите клиента, которого хотите отредактировать", "РЕДАКТИРОВАНИЕ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (SurnameBox.Text == "" || NameBox.Text == "" || PatrBox.Text == "" || BirthDateBox.Text == "" || PhoneBox.Text == "" || PasSeries.Text == "" || PasNumber.Text == "" || DateIssue.Text == "")
                    MessageBox.Show("Все полня должны быть заполнены", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    try
                    {
                        string[] tmp = BirthDateBox.Text.Split('.');
                        string birthDate = String.Format("{0}-{1}-{2}", tmp[2], tmp[1], tmp[0]);

                        CalcAge calcAge = new CalcAge(BirthDateBox.Text);
                        int age = calcAge.Age();

                        tmp = DateIssue.Text.Split('.');
                        string dateIssue = String.Format("{0}-{1}-{2}", tmp[2], tmp[1], tmp[0]);

                        string query = string.Format("UPDATE hotel.client SET surname = '{0}', name = '{1}', patronymic = '{2}', birth_date = '{3}', age  = {4}, phone_number = '{5}', passport_series = '{6}', passport_number = '{7}', date_of_issue = '{8}' WHERE client.IDclient = {9};", SurnameBox.Text, NameBox.Text, PatrBox.Text, birthDate, age, PhoneBox.Text, PasSeries.Text, PasNumber.Text, dateIssue, _id);
                        using (classes.MySql.Connection)
                        {
                            classes.MySql.Connection.Open();

                            MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                            command.ExecuteNonQuery();
                        }

                        LoadDb("SELECT surname AS 'Фамилия', name AS 'Имя', patronymic AS 'Отчество', SUBSTRING(birth_date, 1, 10) AS 'ДатаРождения', age AS 'Возраст', phone_number AS 'Телефон', passport_series AS 'Серия', passport_number AS 'Номер', SUBSTRING(date_of_issue, 1, 10) AS 'ДатаВыдачи' FROM hotel.client;");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    SurnameBox.Clear();
                    NameBox.Clear();
                    PatrBox.Clear();
                    BirthDateBox.Clear();
                    PhoneBox.Clear();
                    PasSeries.Clear();
                    PasNumber.Clear();
                    DateIssue.Clear();
                    _checkedStr = false;
                }
            }
        }

        private void ExpBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using(classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand comand = new MySqlCommand("SELECT * FROM hotel.client;", classes.MySql.Connection);
                    MySqlDataReader reader = comand.ExecuteReader();

                    DataTable clientTable = new DataTable();
                    clientTable.Load(reader);

                    using (StreamWriter writer = new StreamWriter($@"{Directory.GetCurrentDirectory()}\\ExportCsv\\ClientData.csv", false))
                    {
                        for(int columnCount = 0; columnCount <= clientTable.Columns.Count - 1; ++columnCount)
                        {
                            if (columnCount != clientTable.Columns.Count - 1) writer.Write(clientTable.Columns[columnCount].ColumnName + ";");
                            else writer.Write(clientTable.Columns[columnCount].ColumnName);
                        }
                        writer.Write("\n");

                        foreach(DataRow row in clientTable.Rows)
                        {
                            string str = String.Join(";", row.ItemArray);
                            writer.WriteLine(str);
                        }
                    }
                }
                MessageBox.Show("Данные о клиентах выгружены!", "ЭКСПОРТ ДАННЫХ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void ClientPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadDb("SELECT surname AS 'Фамилия', name AS 'Имя', patronymic AS 'Отчество', SUBSTRING(birth_date, 1, 10) AS 'ДатаРождения', age AS 'Возраст', phone_number AS 'Телефон', passport_series AS 'Серия', passport_number AS 'Номер', SUBSTRING(date_of_issue, 1, 10) AS 'ДатаВыдачи' FROM hotel.client;");
        }
    }
}