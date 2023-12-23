using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using siKecil.Infrastructure;

namespace siKecil.View.Main
{
    public partial class Dongeng : Page
    {
        private int ID_Dongeng;
        Connection connectionHelper = new Connection();
        private DongengDisplay dongeng;
        public Dongeng(int ID_Dongeng)
        {
            InitializeComponent();
            this.ID_Dongeng = ID_Dongeng;
            dongeng = GetDongengFromDatabase();
            DisplayDongeng();
        }

        private DongengDisplay GetDongengFromDatabase()
        {
            DongengDisplay dongeng = new DongengDisplay();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                int id_dongeng = this.ID_Dongeng;
                string getDongeng = "SELECT ID_Dongeng, Judul, Isi FROM Dongeng WHERE ID_Dongeng = @ID_Dongeng";

                using (SqlCommand command = new SqlCommand(getDongeng, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID_Dongeng", id_dongeng);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dongeng.Id_Dongeng = reader.GetInt32(0);
                            dongeng.Judul = reader.GetString(1);
                            dongeng.Isi = reader.GetString(2);
                        }
                    }
                }
            }
            return dongeng;
        }

        private void DisplayDongeng()
        {
            string judul = dongeng.Judul;
            string isi = dongeng.Isi;

            JudulDongeng.Text = $"{judul}";
            IsiDongengView.Text = $"{isi}";
        }

        private void DongengPicker_SelectedChanged(object sender, SelectionChangedEventArgs e)
        {
            DisplayDongeng();
        }

        private void NavigateToDongengAnak()
        {
            NavigateToDongengAnak();
        }

        private void DongengAnak_Back(object sender, RoutedEventArgs e)
        {
            NavigateToDongengAnak();
        }
    }

    public class DongengDisplay
    {
        public int Id_Dongeng { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
    }
}
