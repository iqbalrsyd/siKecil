using System;
using System.Windows.Controls;

namespace siKecil.Infrastructure
{
    public static class AgeHelper
    {
        public static int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            int ageInMonths = (currentDate.Year - birthDate.Year) * 12 + currentDate.Month - birthDate.Month;
            if (currentDate.Day < birthDate.Day)
            {
                ageInMonths--;
            }
            return ageInMonths;
        }

        internal static int CalculateAge(object tanggalLahirAnak, DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}
