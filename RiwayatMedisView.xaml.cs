using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace siKecil
{
    /// <summary>
    /// Interaction logic for RiwayatMedis.xaml
    /// </summary>
    public partial class RiwayatMedisView : Window
    {
        private readonly string User_ID;
        private Grid grid;

        public RiwayatMedisView(string user_ID)
        {
            InitializeComponent();
            this.User_ID = user_ID;

            // Tambahkan tulisan "Dashboard" sebagai tautan kembali
            TextBlock ToDashboard_Click = new TextBlock();
            ToDashboard_Click.Text = "Dashboard";
            ToDashboard_Click.FontWeight = FontWeights.Bold;
            ToDashboard_Click.TextDecorations = TextDecorations.Underline;
            ToDashboard_Click.Cursor = Cursors.Hand;
            ToDashboard_Click.MouseLeftButtonDown += ToDashboard_Click_MouseLeftButtonDown; // Ganti nama event handler di sini

        }

        private void ToDashboard_Click_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ToDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(User_ID);
            dashboard.Show();
            this.Close();
        }

        private void SimpanButton_Click(object sender, RoutedEventArgs e)
        {
            // Validasi input sebelum menyimpan
            if (string.IsNullOrEmpty(txtBeratBadan.Text) ||
                string.IsNullOrEmpty(txtTinggi.Text) ||
                string.IsNullOrEmpty(txtLingkarKepala.Text) ||
                cmbGolonganDarah.SelectedItem == null ||
                dpTanggalLahir.SelectedDate == null)
            {
                MessageBox.Show("Harap isi semua kolom yang diperlukan.", "Peringatan", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Ambil nilai dari input
                double beratBadan = Convert.ToDouble(txtBeratBadan.Text);
                double tinggi = Convert.ToDouble(txtTinggi.Text);
                double lingkarKepala = Convert.ToDouble(txtLingkarKepala.Text);
                string riwayatAlergi = txtRiwayatAlergi.Text;
                string riwayatPenyakit = txtRiwayatPenyakit.Text;
                string golonganDarah = ((ComboBoxItem)cmbGolonganDarah.SelectedItem).Content.ToString();

                // Ambil tanggal lahir
                DateTime tanggalLahir = dpTanggalLahir.SelectedDate ?? DateTime.MinValue;

                // Hitung umur
                int umur = DateTime.Now.Year - tanggalLahir.Year;
                if (DateTime.Now < tanggalLahir.AddYears(umur))
                {
                    umur--;
                }

                // Tampilkan umur
                txtUmur.Text = $"Umur: {umur} tahun";

                // Implementasikan logika penyimpanan data ke dalam database atau penyimpanan data lainnya di sini

                // Tampilkan pesan sukses
                MessageBox.Show("Data berhasil disimpan.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Harap masukkan nilai numerik yang valid.", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Kesalahan", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}