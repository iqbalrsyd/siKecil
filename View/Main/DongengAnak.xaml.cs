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
            Title = "Dongeng Anak";
            dongengList = LoadDongeng();
        }

        private ObservableCollection<Cerita> LoadCerita()
        {
            List<Cerita> cerita = new List<Cerita>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                    string query = "SELECT ID_Dongeng, Judul, Isi, Pengarang, TanggalDibuat FROM Dongeng";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cerita cerita1 = new Cerita
                                {
                                    DongengID = Convert.ToInt32(reader["ID_Dongeng"]),
                                    Judul = reader.IsDBNull(reader.GetOrdinal("Judul")) ? string.Empty : reader["Judul"].ToString(),
                                    IsiDongeng = reader.IsDBNull(reader.GetOrdinal("Isi")) ? string.Empty : reader["Isi"].ToString(),
                                    Pengarang = reader.IsDBNull(reader.GetOrdinal("Pengarang")) ? string.Empty : reader["Pengarang"].ToString(),
                                    TanggalDibuat = reader.IsDBNull(reader.GetOrdinal("TanggalDibuat")) ? DateTime.MinValue : Convert.ToDateTime(reader["TanggalDibuat"])
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
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    connection.Open();

                    string query = "INSERT INTO Dongeng (Judul, Isi, Pengarang, TanggalDibuat) VALUES (@Judul, @Isi, @Pengarang, @TanggalDibuat)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Judul", judul);
                        command.Parameters.AddWithValue("@Isi", isiDongeng);
                        command.Parameters.AddWithValue("@Pengarang", pengarang);
                        command.Parameters.AddWithValue("@TanggalDibuat", tanggalDibuat);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void InsertDongeng(string judul, string isi, string gambarPath)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();
                DongengRepository dongengRepo = new DongengRepository();
                dongengRepo.InsertDongeng("Judul Dongeng Baru", "Isi Dongeng Baru", "/Asset/gambar_baru.jpg");


            // Memastikan input tidak kosong sebelum menambahkan ke database
            if (!string.IsNullOrEmpty(judul) && !string.IsNullOrEmpty(isiDongeng))
            {
                // Menambahkan dongeng ke database
                AddDongengToDatabase(judul, isiDongeng, pengarang, tanggalDibuat);

                using (SqlCommand command = new SqlCommand(insertQuery, sqlCon))
                {
                    command.Parameters.AddWithValue("@Judul", "Judul Dongeng Baru");
                    command.Parameters.AddWithValue("@Isi", "Isi Dongeng Baru");
                    command.Parameters.AddWithValue("@GambarPath", "/Asset/gambar_baru.jpg");

                // Membersihkan nilai input setelah ditambahkan ke database
                JudulTextBox.Clear();
                IsiDongengTextBox.Clear();
                PengarangTextBox.Clear();

                NavigationService?.Navigate(new Dongeng());
            }
            else
            {
                MessageBox.Show("Mohon isi semua input.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public class Dongeng
        {
            public int DongengID { get; set; }
            public string Judul { get; set; }
            public string IsiDongeng { get; set; }
            public string Pengarang { get; set; }
            public DateTime TanggalDibuat { get; set; }
        }
    }
}