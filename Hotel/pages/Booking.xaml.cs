using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Hotel.classes;

using MySql.Data.MySqlClient;
//using word = Microsoft.Office.Interop.Word;

namespace Hotel.pages
{
    public partial class Booking
    {
        private readonly string _clientProfile = Directory.GetCurrentDirectory() + @"\template\ClientProfile.docx";
        private readonly string _booking = Directory.GetCurrentDirectory() + @"\template\BookingConfirmation.docx";
        private readonly string _cheque = Directory.GetCurrentDirectory() + @"\template\Cheque.docx";

        private double _totalCost, _foodCost;
        private double _roomCost, _numDays;
        private string _nameRoom = string.Empty;

        private int _idfeed, _idclient, _numroom, _age;

        public Booking()
        {
            InitializeComponent();
        }

        /*private void ReplaceWordStub(string stubToReplace, string text, word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }*/

        #region Формирование документа "Анкета клиента"
        
        private void ClientProfBut_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string surname = Surname.Text;
            string name = _Name.Text;
            string patr = Patr.Text;
            string phoneNumber = PhoneBox.Text;
            string seriesPas = PasSeries.Text, numberPas = PasNumber.Text;
            string date = DateTime.Now.ToShortDateString();
            string birthDay = Birthday.Text;
            string arDate = ForBooking.DataArriv.Substring(0, 10);
            string depDate = ForBooking.DataDepart.Substring(0, 10);
            string issueDate = DateIssue.Text;

            try
            {
                /*var wordApp = new word.Application();
                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(_clientProfile);

                ReplaceWordStub("<surname>", surname, wordDoc);
                ReplaceWordStub("<name>", name, wordDoc);
                ReplaceWordStub("<patr>", patr, wordDoc);
                ReplaceWordStub("<birthday>", birthDay, wordDoc);
                ReplaceWordStub("<phonenumber>", phoneNumber, wordDoc);
                ReplaceWordStub("<seria>", seriesPas, wordDoc);
                ReplaceWordStub("<number>", numberPas, wordDoc);
                ReplaceWordStub("<date>", date, wordDoc);
                ReplaceWordStub("<arrivaldate>", arDate, wordDoc);
                ReplaceWordStub("<departuredate>", depDate, wordDoc);
                ReplaceWordStub("<dateissue>", issueDate, wordDoc);

                wordApp.Visible = true;*/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА");
            }
        }
        
        #endregion

        private void CloseBut_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вернуться на главное в меню?", "ВЫХОД", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new MainPage());
            }
        }

        #region Контроль ввода

        private void FIO_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsLetter(e.Text, 0)))
                e.Handled = true;
        }

        private void PhoneBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) && PhoneBox.Text.Length < 11))
                e.Handled = true;
        }

        private void Date_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Birthday.Text.Length >= 10 || (!(Char.IsDigit(e.Text, 0)) && e.Text != "."))
                e.Handled = true;
        }
        private void Date1_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (DateIssue.Text.Length >= 10 || (!(Char.IsDigit(e.Text, 0)) && e.Text != "."))
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

        #endregion
        
        private void Booking_OnLoaded(object sender, RoutedEventArgs e)
        {
            ArrivalDate.SelectedDate = DateTime.Parse(ForBooking.DataArriv);
            DepartureDate.SelectedDate = DateTime.Parse(ForBooking.DataDepart);
            
            NumOfVis.Text = ForBooking.NumVis.ToString();
            _numDays = GetDays(DateTime.Parse(ForBooking.DataArriv), DateTime.Parse(ForBooking.DataDepart));
            Days.Text = GetDays(DateTime.Parse(ForBooking.DataArriv), DateTime.Parse(ForBooking.DataDepart)).ToString(CultureInfo.CurrentCulture);
            
            FillRoomBox(ForBooking.NumVis);
        }

        private void FillRoomBox(int numOfVis)
        {
            string fillQuery = $"SELECT * FROM hotel.room WHERE visitors_quant >= {numOfVis} AND available_quant >=1 ORDER BY cost ASC;";
            try
            {
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    MySqlCommand command = new MySqlCommand(fillQuery, classes.MySql.Connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    using (reader)
                    {
                        while (reader.Read())
                        {
                            Room.Items.Add($"{reader.GetString(1)} | {reader.GetString(3)} чел. | {reader.GetString(2)} кв.м. ({reader.GetString(4)} руб./ночь)");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private static double GetDays(DateTime start, DateTime end)
        {
            double days = Math.Floor((end - start).TotalDays);
            return days;
        }

        private void Food_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Food.SelectedIndex)
            {
                case 1:
                    _foodCost = 300 * _numDays;
                    _idfeed = 1;
                    break;
                case 2:
                    _foodCost = 600 * _numDays;
                    _idfeed = 2;
                    break;
                case 3:
                    _foodCost = 900 * _numDays;
                    _idfeed = 3;
                    break;
                default:
                    _idfeed = 4;
                    break;
            }
        }

        private void Room_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] Cost = Room.SelectedItem.ToString().Split(' ', '(');
            _nameRoom = Cost[0];
            _roomCost = Double.Parse(Cost[8]) * _numDays;

            using (classes.MySql.Connection)
            {
                classes.MySql.Connection.Open();
                
                MySqlCommand command = new MySqlCommand($"SELECT NUMroom FROM hotel.room WHERE r_name = '{_nameRoom}' AND cost = {Cost[8]};", classes.MySql.Connection);
                _numroom = Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private void calcCost()
        {
            _totalCost = _roomCost + _foodCost;
            AllSum.Text = _totalCost.ToString(CultureInfo.CurrentCulture);
        }
        
        private void CalcCost_OnClick(object sender, RoutedEventArgs e)
        {
            calcCost();
        }

        private void CheckIntoRoomBut_OnClick(object sender, RoutedEventArgs e)
        {
            if (Surname.Text == "" || _Name.Text == "" || Patr.Text == "" || Birthday.Text == "" ||
                PhoneBox.Text == "" || PasSeries.Text == "" || PasNumber.Text == "" || DateIssue.Text == "")
                MessageBox.Show("Заполните все поля с данными о клиенте", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            else if (Food.SelectedItem == null)
                MessageBox.Show("Выберите категорию питания", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            else if (Room.SelectedItem == null)
                MessageBox.Show("Выберите гостиницный номер", "ОШИБКА ЗАПОЛНЕНИЯ", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            else
            {
                calcCost();
                CalcAge calcAge = new CalcAge(Birthday.Text);
                _age = calcAge.Age();

                string[] dateBirth = Birthday.Text.Split('.');
                string[] dateissue = DateIssue.Text.Split('.');

                string[] dateArrival = ArrivalDate.SelectedDate.ToString().Split('.', ' ');
                string[] dateDeparture = DepartureDate.SelectedDate.ToString().Split('.', ' ');

                #region Добавление клиента

                string addClientQuery = $"INSERT INTO hotel.client (surname, name, patronymic, birth_date, age, phone_number, passport_series, passport_number, date_of_issue) VALUES ('{Surname.Text}', '{_Name.Text}', '{Patr.Text}', '{dateBirth[2]}-{dateBirth[1]}-{dateBirth[0]}', {_age}, {PhoneBox.Text}, {PasSeries.Text}, {PasNumber.Text}, '{dateissue[2]}-{dateissue[1]}-{dateissue[0]}'); select last_insert_id();";
                try
                {
                    using (classes.MySql.Connection)
                    {
                        classes.MySql.Connection.Open();

                        MySqlCommand command = new MySqlCommand(addClientQuery, classes.MySql.Connection);
                        _idclient = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                #endregion

                #region Добавление записи о бронировании номера

                int quantRoom = 0;
                using (classes.MySql.Connection)
                {
                    classes.MySql.Connection.Open();

                    string findQuantRoom = $"SELECT available_quant FROM hotel.room WHERE NUMroom = {_numroom};";
                    MySqlCommand command = new MySqlCommand(findQuantRoom, classes.MySql.Connection);
                    quantRoom = Convert.ToInt32(command.ExecuteScalar());
                }
                
                string bookingQuery = $"INSERT INTO hotel.booking (arrival_date, departure_date, IDclient, NUMroom, count_day, IDfeed, amount) VALUES ('{dateArrival[2]}-{dateArrival[1]}-{dateArrival[0]}', '{dateDeparture[2]}-{dateDeparture[1]}-{dateDeparture[0]}', {_idclient}, {_numroom}, {_numDays}, {_idfeed}, {_totalCost});";
                classes.MySql.AddCommand(bookingQuery);
                if (ForBooking.DataArriv.Substring(0, 10).Equals(DateTime.Now.ToShortDateString()))
                {
                    string updateRoomInfo = $"UPDATE hotel.room SET available_quant = {quantRoom - 1} WHERE NUMroom = {_numroom};";
                    classes.MySql.AddCommand(updateRoomInfo);
                    CreateWordDocCheque();
                }
                else
                {
                    MessageBox.Show("Бронирование (возможно другой код)");
                    CreateWordDocBooking();
                }
                #endregion
            }
        }

        private void CreateWordDocBooking()
        {
            string surname = Surname.Text;
            string name = _Name.Text;
            string patr = Patr.Text;
            string datenow = DateTime.Now.ToShortDateString(), timenow = DateTime.Now.ToShortTimeString();
            string arDate = ForBooking.DataArriv.Substring(0, 10);
            string depDate = ForBooking.DataDepart.Substring(0, 10);
            string residents = ForBooking.NumVis.ToString();
            string food = Food.Text;
            string room = Room.Text;
            string nights = Days.Text;

            try
            {
                /*var wordApp = new word.Application();
                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(_booking);

                ReplaceWordStub("<surname>", surname, wordDoc);
                ReplaceWordStub("<name>", name, wordDoc);
                ReplaceWordStub("<patr>", patr, wordDoc);
                ReplaceWordStub("<date_now>, ", datenow, wordDoc);
                ReplaceWordStub("<time_now>", timenow, wordDoc);
                ReplaceWordStub("<n>", name.Substring(0, name.Length - (name.Length - 1)), wordDoc);
                ReplaceWordStub("<p>.", patr.Substring(0, patr.Length - (patr.Length - 1)), wordDoc);
                ReplaceWordStub("<surname>", surname, wordDoc);
                ReplaceWordStub("<arrival_date>", arDate, wordDoc);
                ReplaceWordStub("<departure_date>", depDate, wordDoc);
                ReplaceWordStub("<residents>", residents, wordDoc);
                ReplaceWordStub("<food>", food, wordDoc);
                ReplaceWordStub("<room>", room, wordDoc);
                ReplaceWordStub("<days>", nights, wordDoc);

                wordApp.Visible = true;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА");
            }
        }

        private void CreateWordDocCheque()
        {
            string client = $"{Surname.Text} {_Name.Text} {Patr.Text}";
            string datenow = DateTime.Now.ToShortDateString();
            string arDate = ForBooking.DataArriv.Substring(0, 10);
            string depDate = ForBooking.DataDepart.Substring(0, 10);
            string food = Food.Text;
            string room = Room.Text;
            string nights = Days.Text;
            string amount = AllSum.Text;

            Random random = new Random();
            string regNum = random.Next(0, 900).ToString();
            
            try
            {
                /*var wordApp = new word.Application();
                wordApp.Visible = false;
                var wordDoc = wordApp.Documents.Open(_booking);

                ReplaceWordStub("<client>", client, wordDoc);
                ReplaceWordStub("<date_now>", datenow, wordDoc);
                ReplaceWordStub("<arrival_date>", arDate, wordDoc);
                ReplaceWordStub("<departure_date>", depDate, wordDoc);
                ReplaceWordStub("<food>", food, wordDoc);
                ReplaceWordStub("<room>", room, wordDoc);
                ReplaceWordStub("<days>", nights, wordDoc);
                ReplaceWordStub("<amount>", amount, wordDoc);
                ReplaceWordStub("<regnum>", regNum, wordDoc);
                ReplaceWordStub("<cost_room>", amount, wordDoc);
                ReplaceWordStub("<cost_feed>", regNum, wordDoc);

                wordApp.Visible = true;*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА");
            }
        }
    }
}