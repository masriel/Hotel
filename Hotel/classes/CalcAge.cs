using System;

namespace Hotel.classes
{
    public class CalcAge
    {
        static DateTime _bYear;
        static DateTime _nowYear = DateTime.Today;

        public CalcAge(string byear)
        {
            _bYear = Convert.ToDateTime(byear);
        }

        public int Age()
        {
            int age = _nowYear.Year - _bYear.Year;
            if (_bYear > _nowYear.AddYears(-age)) age--;

            return age;
        }
    }
}
