using System;

namespace Hotel.classes
{
    public class CalcAge
    {
        static DateTime BYear;
        static DateTime NowYear = DateTime.Today;

        public CalcAge(string byear)
        {
            BYear = Convert.ToDateTime(byear);
        }

        public int Age()
        {
            int age = NowYear.Year - BYear.Year;
            if (BYear > NowYear.AddYears(-age)) age--;

            return age;
        }
    }
}
