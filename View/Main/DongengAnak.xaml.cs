using siKecil.Infrastructure;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace siKecil.View.Main
{
    public partial class DongengAnakPage : Page
    {
        private ObservableCollection<Dongeng> dongengList;
        private Connection connectionHelper = new Connection();

        public DongengAnakPage()
        {
            InitializeComponent();
            dongengList = LoadDongeng();
        }

        private ObservableCollection<Dongeng> LoadDongeng()
        {
            ObservableCollection<Dongeng> dongengs = new ObservableCollection<Dongeng>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                    string query = "SELECT DongengID, Judul, IsiDongeng, Pengarang, TanggalDibuat FROM DongengAnak";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dongeng dongeng = new Dongeng
                                {
                                    DongengID = Convert.ToInt32(reader["DongengID"]),
                                    Judul = reader.IsDBNull(reader.GetOrdinal("Judul")) ? string.Empty : reader["Judul"].ToString(),
                                    IsiDongeng = reader.IsDBNull(reader.GetOrdinal("IsiDongeng")) ? string.Empty : reader["IsiDongeng"].ToString(),
                                    Pengarang = reader.IsDBNull(reader.GetOrdinal("Pengarang")) ? string.Empty : reader["Pengarang"].ToString(),
                                    TanggalDibuat = reader.IsDBNull(reader.GetOrdinal("TanggalDibuat")) ? DateTime.MinValue : Convert.ToDateTime(reader["TanggalDibuat"])
                                };

                                dongengs.Add(dongeng);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return dongengs;
        }

        private void AddDongengToDatabase(string judul, string isiDongeng, string pengarang, DateTime tanggalDibuat)
        {
            try
            {
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    connection.Open();

                    string query = "INSERT INTO DongengAnak (Judul, IsiDongeng, Pengarang, TanggalDibuat) VALUES (@Judul, @IsiDongeng, @Pengarang, @TanggalDibuat)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Judul", judul);
                        command.Parameters.AddWithValue("@IsiDongeng", isiDongeng);
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

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan nilai dari input
            string judul = JudulTextBox.Text;
            string isiDongeng = IsiDongengTextBox.Text;
            string pengarang = PengarangTextBox.Text;
            DateTime tanggalDibuat = DateTime.Now; // Menggunakan tanggal saat ini

            // Memastikan input tidak kosong sebelum menambahkan ke database
            if (!string.IsNullOrEmpty(judul) && !string.IsNullOrEmpty(isiDongeng) )
            {
                // Menambahkan dongeng ke database
                AddDongengToDatabase(judul, isiDongeng, pengarang, tanggalDibuat);

                // Memuat ulang dongeng dari database
                dongengList.Clear();
                foreach (var dongeng in LoadDongeng())
                {
                    dongengList.Add(dongeng);
                }

                // Membersihkan nilai input setelah ditambahkan ke database
                JudulTextBox.Clear();
                IsiDongengTextBox.Clear();
                PengarangTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Mohon isi semua input.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
