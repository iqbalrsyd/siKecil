using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Media3D;

namespace siKecil
{
    public class Criteria
    {
        public string Gender { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int AgeInMonths { get; set; }
        public double LingkarKepala { get; set; }
        public string conditionChild { get; set; }
        public string statusLK { get; set; }

        public Criteria(string JenisKelaminAnak, double weight, double height, int age, double lingkarKepala)
        {
            Gender = JenisKelaminAnak;
            Weight = weight;
            Height = height;
            AgeInMonths = age;
            LingkarKepala = lingkarKepala;
        }


        public string CalculateGizi()
        {
            string conditionChild = string.Empty;
            double resultIMT = Weight / ((Height / 100) * (Height / 100));
            if (AgeInMonths >= 0 && AgeInMonths <= 9)
            {
                if (Gender == "Laki-Laki")
                {
                    switch (AgeInMonths)
                    {
                        case 0:
                            if (resultIMT < 10.2)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 10.2 && resultIMT < 11.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 11.1 && resultIMT < 14.8)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 14.8 && resultIMT < 16.3)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 16.3 && resultIMT < 18.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 18.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 1:
                            if (resultIMT < 11.3)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 11.3 && resultIMT < 12.4)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 12.4 && resultIMT < 16.3)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 16.3 && resultIMT < 17.8)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 17.8 && resultIMT < 19.4)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 19.4)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 2:
                            if (resultIMT < 12.5)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 12.5 && resultIMT < 13.7)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 13.7 && resultIMT < 17.8)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 17.8 && resultIMT < 19.4)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 19.4 && resultIMT < 21.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 21.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 3:
                            if (resultIMT < 13.1)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.1 && resultIMT < 14.3)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.3 && resultIMT < 18.4)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.4 && resultIMT < 20)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20 && resultIMT < 21.8)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 21.8)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 4:
                            if (resultIMT < 13.4)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.4 && resultIMT < 14.5)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.5 && resultIMT < 18.7)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.7 && resultIMT < 20.3)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.3 && resultIMT < 22.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 5:
                            if (resultIMT < 13.5)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.5 && resultIMT < 14.7)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.7 && resultIMT < 18.8)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.8 && resultIMT < 20.5)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.5 && resultIMT < 22.3)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.3)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 6:
                            if (resultIMT < 13.6)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.6 && resultIMT < 14.7)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.7 && resultIMT < 18.8)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.8 && resultIMT < 20.5)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.5 && resultIMT < 22.3)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.3)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 7:
                            if (resultIMT < 13.7)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.7 && resultIMT < 14.8)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.8 && resultIMT < 18.8)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.8 && resultIMT < 20.5)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.5 && resultIMT < 22.3)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.3)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 8:
                            if (resultIMT < 13.6)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.6 && resultIMT < 14.8)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.8 && resultIMT < 18.7)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.7 && resultIMT < 20.4)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.4 && resultIMT < 22.2)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.2)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 9:
                            if (resultIMT < 13.6)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13.6 && resultIMT < 14.7)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.7 && resultIMT < 18.6)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.6 && resultIMT < 20.3)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.3 && resultIMT < 22.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        default:
                            conditionChild = "Data tidak valid";
                            break;
                    }
                }
                else if (Gender == "Perempuan")
                {
                    switch (AgeInMonths)
                    {
                        case 0:
                            if (resultIMT < 10.1)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 10.1 && resultIMT < 11.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 11.1 && resultIMT < 14.6)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 14.6 && resultIMT < 16.1)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 16.1 && resultIMT < 17.7)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 17.7)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 1:
                            if (resultIMT < 10.8)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 10.8 && resultIMT < 12)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 12 && resultIMT < 16)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 16 && resultIMT < 17.5)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 17.5 && resultIMT < 19.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 19.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 2:
                            if (resultIMT < 11.8)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 11.8 && resultIMT < 13)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 13 && resultIMT < 17.3)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 17.3 && resultIMT < 19)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 19 && resultIMT < 20.7)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 20.7)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 3:
                            if (resultIMT < 12.4)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 12.4 && resultIMT < 13.6)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 13.6 && resultIMT < 17.9)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 17.9 && resultIMT < 19.7)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 19.7 && resultIMT < 21.5)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 21.5)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 4:
                            if (resultIMT < 12.7)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 12.7 && resultIMT < 13.9)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 13.9 && resultIMT < 18.3)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.3 && resultIMT < 20)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20 && resultIMT < 22)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 5:
                            if (resultIMT < 12.9)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 12.9 && resultIMT < 14.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.1 && resultIMT < 18.4)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.4 && resultIMT < 20.2)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.2 && resultIMT < 22.2)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.2)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 6:
                            if (resultIMT < 13)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13 && resultIMT < 14.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.1 && resultIMT < 18.5)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.5 && resultIMT < 20.3)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.3 && resultIMT < 22.3)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.3)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 7:
                            if (resultIMT < 13)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13 && resultIMT < 14.2)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.2 && resultIMT < 18.5)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.5 && resultIMT < 20.3)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.3 && resultIMT < 22.3)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.3)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 8:
                            if (resultIMT < 13)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 13 && resultIMT < 14.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.1 && resultIMT < 18.4)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.4 && resultIMT < 20.2)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.2 && resultIMT < 22.2)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.2)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        case 9:
                            if (resultIMT < 12.9)
                            {
                                conditionChild = "Gizi Buruk";
                            }
                            else if (resultIMT >= 12.9 && resultIMT < 14.1)
                            {
                                conditionChild = "Gizi Kurang";
                            }
                            else if (resultIMT >= 14.1 && resultIMT < 18.3)
                            {
                                conditionChild = "Gizi Baik";
                            }
                            else if (resultIMT >= 18.3 && resultIMT < 20.1)
                            {
                                conditionChild = "Berisiko Gizi Lebih";
                            }
                            else if (resultIMT >= 20.1 && resultIMT < 22.1)
                            {
                                conditionChild = "Gizi Lebih";
                            }
                            else if (resultIMT >= 22.1)
                            {
                                conditionChild = "Obesitas";
                            }
                            break;
                        default:
                            conditionChild = "Data yang tersedia tidak valid";
                            break;
                    }
                }
                else
                {
                    conditionChild = "Data tidak valid";
                }
            }
            else
            {
                conditionChild = "Umur tidak ada di dalam kriteria";
            }
            return conditionChild;
        }

        public string CalculateLK()
        {
            string statusLK = string.Empty;
            if (AgeInMonths >= 0 && AgeInMonths <= 9)
            {
                if (Gender == "Laki-Laki")
                {
                    switch (AgeInMonths)
                    {
                        case 0:
                            if (LingkarKepala < 31.9)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 31.9 && LingkarKepala < 37)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 37)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 1:
                            if (LingkarKepala < 34.9)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 34.9 && LingkarKepala < 39.6)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 39.6)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 2:
                            if (LingkarKepala < 36.8)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 36.8 && LingkarKepala < 41.5)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 41.5)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 3:
                            if (LingkarKepala < 38.1)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 38.1 && LingkarKepala < 42.9)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 42.9)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 4:
                            if (LingkarKepala < 39.2)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 39.2 && LingkarKepala < 44)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 44)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 5:
                            if (LingkarKepala < 40.1)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 40.1 && LingkarKepala < 45)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 45)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 6:
                            if (LingkarKepala < 40.9)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 40.9 && LingkarKepala < 45.8)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 45.8)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 7:
                            if (LingkarKepala < 41.5)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 41.5 && LingkarKepala < 46.4)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 46.4)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 8:
                            if (LingkarKepala < 42)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 42 && LingkarKepala < 47)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 47)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 9:
                            if (LingkarKepala < 42.5)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 42.5 && LingkarKepala < 47.5)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 47.5)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        default:
                            statusLK = "Data tidak valid";
                            break;
                    }
                }
                else if (Gender == "Perempuan")
                {
                    switch (AgeInMonths)
                    {
                        case 0:
                            if (LingkarKepala < 31.5)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 31.5 && LingkarKepala < 36.2)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 36.2)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 1:
                            if (LingkarKepala < 34.2)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 34.2 && LingkarKepala < 38.9)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 38.9)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 2:
                            if (LingkarKepala < 35.8)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 35.8 && LingkarKepala < 40.7)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 40.7)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 3:
                            if (LingkarKepala < 37.1)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 37.1 && LingkarKepala < 42)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 42)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 4:
                            if (LingkarKepala < 38.1)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 38.1 && LingkarKepala < 43.1)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 43.1)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 5:
                            if (LingkarKepala < 38.9)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 38.9 && LingkarKepala < 44)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 44)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 6:
                            if (LingkarKepala < 39.6)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 39.6 && LingkarKepala < 44.8)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 44.8)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 7:
                            if (LingkarKepala < 40.2)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 40.2 && LingkarKepala < 45.5)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 45.5)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 8:
                            if (LingkarKepala < 40.7)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 40.7 && LingkarKepala < 46)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 46)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        case 9:
                            if (LingkarKepala < 41.2)
                            {
                                statusLK = "Mikrosefali";
                            }
                            else if (LingkarKepala >= 41.2 && LingkarKepala < 41.2)
                            {
                                statusLK = "Normal";
                            }
                            else if (LingkarKepala >= 41.2)
                            {
                                statusLK = "Makrosefali";
                            }
                            break;
                        default:
                            statusLK = "Data tidak valid";
                            break;
                    }
                }
                else
                {
                    statusLK = "Data tidak valid";
                }
            }
            else
            {
                statusLK = "Umur tidak ada di dalam kriteria";
            }
            return statusLK;
        }
    }
}
