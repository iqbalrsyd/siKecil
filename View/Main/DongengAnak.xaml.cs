using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using siKecil.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace siKecil.View.Main
{
    /// <summary>
    /// Interaction logic for DongengAnak.xaml
    /// </summary>
    public partial class DongengAnak : Page
    {
        private ObservableCollection<Cerita> Ceritas;
        Connection connectionHelper = new Connection();

        public DongengAnak()
        {
            InitializeComponent();
            Title = "Dongeng Anak-Anak";
            Ceritas = LoadCerita();
            ListBoxCerita.ItemsSource = Ceritas;
        }

        private ObservableCollection<Cerita> LoadCerita()
        {
            List<Cerita> cerita = new List<Cerita>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                    // Mengganti query SQL
                    string query = $"SELECT ID_Cerita, Judul, Isi FROM Dongeng";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cerita cerita1 = new Cerita
                                {
                                    ID_Cerita = Convert.ToInt32(reader["ID_Cerita"]),
                                    Judul = reader.IsDBNull(reader.GetOrdinal("Judul")) ? string.Empty : reader["Judul"].ToString(),
                                    Isi = reader.IsDBNull(reader.GetOrdinal("Isi")) ? string.Empty : reader["Isi"].ToString()
                                };

                                cerita.Add(cerita1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return new ObservableCollection<Cerita>(cerita);
        }

        private void CeritaList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCerita.SelectedItem != null)
            {
                Cerita selectedCerita = (Cerita)ListBoxCerita.SelectedItem;
                int selectedCeritaID = selectedCerita.ID_Cerita;
                NavigationService?.Navigate(new Dongeng(selectedCeritaID));
            }
        }
    }

    public class DongengRepository
    {
        private Connection connectionHelper;


        public DongengRepository()
        {
            connectionHelper = new Connection();
        }

        public void InsertDongeng(string judul, string isi, string gambarPath)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();
                DongengRepository dongengRepo = new DongengRepository();
                dongengRepo.InsertDongeng("Judul Dongeng Baru", "Isi Dongeng Baru", "/Asset/gambar_baru.jpg");


                string insertQuery = "INSERT INTO Dongeng (Judul, Isi, GambarPath) VALUES (@Judul, @Isi, @GambarPath)";

                using (SqlCommand command = new SqlCommand(insertQuery, sqlCon))
                {
                    command.Parameters.AddWithValue("@Judul", "Judul Dongeng Baru");
                    command.Parameters.AddWithValue("@Isi", "Isi Dongeng Baru");
                    command.Parameters.AddWithValue("@GambarPath", "/Asset/gambar_baru.jpg");

                    command.ExecuteNonQuery();
                }
            }
        }
    }



    public class Cerita
    {
        public int ID_Cerita { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
    }
}
