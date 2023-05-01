using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.IO;

using MySql.Data.MySqlClient;

namespace Hotel.pages
{
    public partial class RoomPage
    {
        private readonly DataTable _table = new DataTable();
        private int _id;
        private bool _checkedStr;
        
        public RoomPage()
        {
            InitializeComponent();
        }

        //загрузка данных из базы
        private void LoadDb(string query)
        {
            try
            {
                _table.Clear();

                using(classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand command= new MySqlCommand(query, classes.MySql.Connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(_table);
                    RoomDG.ItemsSource = _table.DefaultView;
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        //кнопка возвращения в главное меню
        private void ReturnBut_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Хотите закрыть панель управления гостиничными номерами?", "ВЫХОД", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        //получение ID выбранной строки 
        private void RoomDG_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _checkedStr = true;
            try
            {
                _id = Convert.ToInt32(_table.DefaultView[RoomDG.SelectedIndex]["№"]);
            }
            catch(Exception)
            {
                MessageBox.Show("Некорректно выбрана строка", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (_id != -1)
            {
                try
                {
                    using(classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        MySqlCommand command= new MySqlCommand(String.Format("SELECT * FROM hotel.room WHERE NUMroom = {0}", _id), classes.MySql.Connection);
                        MySqlDataReader reader  = command.ExecuteReader();

                        while (reader.Read())
                        {
                            _id= Convert.ToInt32(reader.GetString(0));
                            NameBox.Text = reader.GetString(1);
                            AreaBox.Text = reader.GetString(2);
                            VisitQuant.Text = reader.GetString(3);
                            CostBox.Text = reader.GetString(4);
                            AvailableQuant.Text = reader.GetString(5);
                        }
                    }
                }
                catch(Exception ex )
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #region Контроль ввода
        
        private void NameBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void AreaBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
                e.Handled = true;
        }
        
        #endregion

        //обновление данных в базе
        private void DoneBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(_checkedStr == false)
                MessageBox.Show("Выберите номер, данные о котором хотите отредактировать", "Редактирование номера ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if(NameBox.Text == "" || AreaBox.Text == "" || CostBox.Text == "" || VisitQuant.Text == "" || AvailableQuant.Text == "")
                    MessageBox.Show("Все полня должны быть заполнены", "Ошибка заполнения", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    try
                    {
                        string query = String.Format("UPDATE hotel.room SET r_name = '{0}', area = {1}, visitors_quant = {2}, cost = {3}, available_quant = {4} WHERE room.NUMroom = {5};", NameBox.Text, AreaBox.Text, VisitQuant.Text, CostBox.Text, AvailableQuant.Text, _id);
                        using(classes.MySql.Connection)
                        {
                            classes.MySql.Connection.Open();

                            MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                            command.ExecuteNonQuery();
                        }
                        LoadDb("SELECT NUMroom AS '№', r_name AS 'Название', area AS 'Площадь', visitors_quant AS 'Посетителей', cost AS 'Стоимость', available_quant AS 'Свободных' FROM hotel.room;");

                        NameBox.Clear();
                        AreaBox.Clear();
                        CostBox.Clear();
                        VisitQuant.Text = String.Empty;
                        AvailableQuant.Text = String.Empty;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        //добавление новой записи в базу
        private void AddBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(_checkedStr)
            {
                MessageBox.Show("Нельзя добавить уже сущестующий номер", "ОШИБКА ДОБАВЛЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Information);
                NameBox.Clear();
                AreaBox.Clear();
                CostBox.Clear();
                VisitQuant.Text = String.Empty;
                AvailableQuant.Text = String.Empty;
                _checkedStr = false;
            }
            else 
            {
                if (NameBox.Text == "" || AreaBox.Text == "" || CostBox.Text == "" || VisitQuant.Text == "" || AvailableQuant.Text == "")
                    MessageBox.Show("Все полня должны быть заполнены", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    try
                    {
                        string query = String.Format("INSERT INTO hotel.room (r_name, area, visitors_quant, cost, available_quant) VALUES ('{0}', {1}, {2}, {3}, {4});", NameBox.Text, AreaBox.Text, VisitQuant.Text, CostBox.Text, AvailableQuant.Text);
                        using (classes.MySql.Connection)
                        {
                            classes.MySql.Connection.Open();

                            MessageBoxResult result = MessageBox.Show("Вы действительно хотите добавить новую комнату в базу?", "ДОБАВЛЕНИЕ ДАННЫХ", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {
                                MySqlCommand command = new MySqlCommand(query, classes.MySql.Connection);
                                command.ExecuteNonQuery();

                                NameBox.Clear();
                                AreaBox.Clear();
                                CostBox.Clear();
                                VisitQuant.Text = String.Empty;
                                AvailableQuant.Text = String.Empty;
                            }
                        }
                        LoadDb("SELECT NUMroom AS '№', r_name AS 'Название', area AS 'Площадь', visitors_quant AS 'Посетителей', cost AS 'Стоимость', available_quant AS 'Свободных' FROM hotel.room;");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        //удаление выбранной записи
        private void DeleteBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(_checkedStr == false)
                MessageBox.Show("Выберите номер который хотите удалить из базы", "УДАЛЕНИЕ", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    using(classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить этот номер из базы?", "УДАЛЕНИЕ", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if(result == MessageBoxResult.Yes)
                        {
                            MySqlCommand command = new MySqlCommand(String.Format("DELETE FROM hotel.room WHERE NUMroom = {0};", _id), classes.MySql.Connection);
                            command.ExecuteNonQuery();

                            NameBox.Clear();
                            AreaBox.Clear();
                            CostBox.Clear();
                            VisitQuant.Text = String.Empty;
                            AvailableQuant.Text = String.Empty;
                        }
                    }
                    LoadDb("SELECT NUMroom AS '№', r_name AS 'Название', area AS 'Площадь', visitors_quant AS 'Посетителей', cost AS 'Стоимость', available_quant AS 'Свободных' FROM hotel.room;");

                    _checkedStr = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //загрузка формы
        private void RoomPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadDb("SELECT NUMroom AS '№', r_name AS 'Название', area AS 'Площадь', visitors_quant AS 'Посетителей', cost AS 'Стоимость', available_quant AS 'Свободных' FROM hotel.room;");
        }

        //эскпорт данных
        private void ExpBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using(classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand comand = new MySqlCommand("SELECT * FROM hotel.room;", classes.MySql.Connection);
                    MySqlDataReader reader = comand.ExecuteReader();

                    DataTable roomTable = new DataTable();
                    roomTable.Load(reader);

                    using (StreamWriter writer = new StreamWriter($@"{Directory.GetCurrentDirectory()}\\ExportCsv\\RoomData.csv", false))
                    {
                        for(int columnCount = 0; columnCount <= roomTable.Columns.Count - 1; ++columnCount)
                        {
                            if (columnCount != roomTable.Columns.Count - 1) writer.Write(roomTable.Columns[columnCount].ColumnName + ";");
                            else writer.Write(roomTable.Columns[columnCount].ColumnName);
                        }
                        writer.Write("\n");

                        foreach(DataRow row in roomTable.Rows)
                        {
                            string str = String.Join(";", row.ItemArray);
                            writer.WriteLine(str);
                        }
                    }
                }
                MessageBox.Show("Данные о номерах гостиницы выгружены!", "ЭКСПОРТ ДАННЫХ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}