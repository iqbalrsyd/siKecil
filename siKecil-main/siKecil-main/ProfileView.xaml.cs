using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.IO;

namespace siKecil   
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : Window
    {
        private readonly string User_ID;
        Connection connectionHelper = new Connection();

        public ProfileView(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            LoadProvincesAsync();
        }

        private void NamaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EditProfileButton(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        string updateQuery = "UPDATE Users SET EmailAddress = @EmailAddress, Password = @Password, FirstName=@FirstName, LastName=@LastName WHERE User_ID = @User_ID";
                        using (SqlCommand cmd1 = new SqlCommand(updateQuery, sqlCon, transaction))
                        {
                            cmd1.Parameters.AddWithValue("@EmailAddress", txtEditEmailAddress.Text);
                            cmd1.Parameters.AddWithValue("@Password", txtEditPassword.Password);
                            cmd1.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
                            cmd1.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
                            cmd1.Parameters.AddWithValue("@User_ID", User_ID);
                            cmd1.ExecuteNonQuery();
                        }

                        string checkQuery2 = "SELECT COUNT(*) FROM ProfileOrangTua WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd2 = new SqlCommand(checkQuery2, sqlCon, transaction))
                        {
                            checkCmd2.Parameters.AddWithValue("@User_ID", User_ID);
                            int count2 = (int)checkCmd2.ExecuteScalar();

                            if (count2 > 0)
                            {
                                string updateQuery2 = "UPDATE ProfileOrangTua SET TanggalLahir=@TanggalLahir, Alamat=@Alamat, JenisKelamin=@JenisKelamin, NomorTelepon=@NomorTelepon, " +
                                                      "HubunganDenganAnak=@HubunganDenganAnak WHERE User_Id = @User_ID";
                                using (SqlCommand cmd2 = new SqlCommand(updateQuery2, sqlCon, transaction))
                                {
                                    cmd2.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd2.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd2.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd2.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery2 = "INSERT INTO ProfileOrangTua (User_Id, TanggalLahir, Alamat, JenisKelamin, NomorTelepon, HubunganDenganAnak) " +
                                                      "VALUES (@User_ID, @TanggalLahir, @Alamat, @JenisKelamin, @NomorTelepon, @HubunganDenganAnak)";
                                using (SqlCommand cmd2 = new SqlCommand(insertQuery2, sqlCon, transaction))
                                {
                                    cmd2.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd2.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd2.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd2.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd2.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd2.ExecuteNonQuery();
                                }
                            }

                        }

                        string checkQuery3 = "SELECT COUNT(*) FROM ProfileAnak WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd3 = new SqlCommand(checkQuery3, sqlCon, transaction))
                        {
                            checkCmd3.Parameters.AddWithValue("@User_ID", User_ID);
                            int count3 = (int)checkCmd3.ExecuteScalar();

                            if (count3 > 0)
                            {
                                string updateQuery3 = "UPDATE ProfileAnak SET TanggalLahirAnak=@TanggalLahirAnak, NamaAnak=@NamaAnak, NamaPanggilanAnak=@NamaPanggilanAnak, JenisKelaminAnak=@JenisKelaminAnak WHERE User_Id = @User_ID";
                                using (SqlCommand cmd3 = new SqlCommand(updateQuery3, sqlCon, transaction))
                                {
                                    cmd3.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.DisplayDate);
                                    cmd3.Parameters.AddWithValue("@NamaAnak", txtEditNamaAnak.Text);
                                    cmd3.Parameters.AddWithValue("@NamaPanggilanAnak", txtEditNamaPanggilanAnak.Text);
                                    cmd3.Parameters.AddWithValue("@JenisKelaminAnak", JenisKelaminAnakComboBox.Text);
                                    cmd3.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd3.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery3 = "INSERT INTO ProfileAnak (User_Id, TanggalLahirAnak, NamaAnak, NamaPanggilanAnak, JenisKelaminAnak) " +
                                                      "VALUES (@User_ID, @TanggalLahirAnak, @NamaAnak, @NamaPanggilanAnak, @JenisKelaminAnak)";
                                using (SqlCommand cmd3 = new SqlCommand(insertQuery3, sqlCon, transaction))
                                {
                                    cmd3.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd3.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.DisplayDate);
                                    cmd3.Parameters.AddWithValue("@NamaAnak", txtEditNamaAnak.Text);
                                    cmd3.Parameters.AddWithValue("@NamaPanggilanAnak", txtEditNamaPanggilanAnak.Text);
                                    cmd3.Parameters.AddWithValue("@JenisKelaminAnak", JenisKelaminAnakComboBox.Text);
                                    cmd3.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Changes saved successfully!");
                        MainWindow dashboard = new MainWindow(User_ID);
                        dashboard.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaksi jika terjadi kesalahan
                        transaction.Rollback();
                        MessageBox.Show("Error saving changes: " + ex.Message);
                    }
                }
            }
        }


        private void HubunganDenganAnakComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JenisKelaminOrangTuaComboBox.SelectedItem != null && HubunganDenganAnakComboBox.SelectedItem != null)
            {
                string selectedOption = ((ComboBoxItem)HubunganDenganAnakComboBox.SelectedItem).Content.ToString();
            }
        }

        private void JenisKelaminOrangTuaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JenisKelaminOrangTuaComboBox.SelectedItem != null)
            {
                string selectedOption = ((ComboBoxItem)JenisKelaminOrangTuaComboBox.SelectedItem).Content.ToString();
            }
        }
        private void JenisKelaminAnakComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JenisKelaminAnakComboBox.SelectedItem != null)
            {
                string selectedOption = ((ComboBoxItem)JenisKelaminAnakComboBox.SelectedItem).Content.ToString();
            }
        }

        private void NoEditProfielButton(object sender, RoutedEventArgs e)
        {
            txtEditEmailAddress.Text = "";
            txtEditPassword.Password = "";

            txtEditFirstName.Text = "";
            txtEditLastName.Text = "";
            DateTanggalLahirOrangTua.SelectedDate = null;
            txtEditAlamat.Text = "";
            txtEditNomorTelepon.Text = "";

            DateTanggalLahirAnak.SelectedDate = null;
            txtEditNamaAnak.Text = "";
            txtEditNamaPanggilanAnak.Text = "";

            HubunganDenganAnakComboBox.SelectedIndex = -1;
            JenisKelaminOrangTuaComboBox.SelectedIndex = -1;
            JenisKelaminAnakComboBox.SelectedIndex = -1;

            MessageBox.Show("Perubahan dibatalkan. Tampilan dikembalikan ke keadaan awal.");
        }

        private void ToDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(User_ID);
            dashboard.Show();
            this.Close();
        }

        private async void LoadProvincesAsync()
        {
            try
            {
                string apiUrl = "https://iqbalrsyd.github.io/api-wilayah-indonesia/api/provinces.json";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonContent);
                        ProvinsiComboBox.ItemsSource = provinces;
                    }
                    else
                    {
                        MessageBox.Show($"Failed to retrieve provinces. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void ProvinsiComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Province selectedProvince = ProvinsiComboBox.SelectedItem as Province;

                if (selectedProvince != null)
                {
                    string apiUrl = $"https://iqbalrsyd.github.io/api-wilayah-indonesia/api/regencies/{selectedProvince.Id}.json";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            var regencies = JsonConvert.DeserializeObject<List<Regency>>(jsonContent);
                            KabKotaComboBox.ItemsSource = regencies;
                        }
                        else
                        {
                            MessageBox.Show($"Failed to retrieve regencies. Status code: {response.StatusCode}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a province first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private async void KabKotaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Regency selectedRegency = KabKotaComboBox.SelectedItem as Regency;

                if (selectedRegency != null)
                {
                    string apiUrl = $"https://iqbalrsyd.github.io/api-wilayah-indonesia/api/districts/{selectedRegency.Id}.json";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            var districts = JsonConvert.DeserializeObject<List<District>>(jsonContent);
                            KecamatanComboBox.ItemsSource = districts;
                        }
                        else
                        {
                            MessageBox.Show($"Failed to retrieve districts. Status code: {response.StatusCode}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a regency first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private async void KecamatanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                District selectedDistrict = KecamatanComboBox.SelectedItem as District;

                if (selectedDistrict != null)
                {
                    string apiUrl = $"https://iqbalrsyd.github.io/api-wilayah-indonesia/api/villages/{selectedDistrict.Id}.json";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            var villages = JsonConvert.DeserializeObject<List<Village>>(jsonContent);
                            KelurahanComboBox.ItemsSource = villages;
                        }
                        else
                        {
                            MessageBox.Show($"Failed to retrieve villages. Status code: {response.StatusCode}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a district first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private async void KelurahanComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Village selectedVillage = KelurahanComboBox.SelectedItem as Village;

                if (selectedVillage != null)
                {
                    string apiUrl = $"https://iqbalrsyd.github.io/api-wilayah-indonesia/api/villages/{selectedVillage.Id}.json";

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            var villages = JsonConvert.DeserializeObject<List<Village>>(jsonContent);
                            KelurahanComboBox.ItemsSource = villages;
                        }
                        else
                        {
                            MessageBox.Show($"Failed to retrieve villages. Status code: {response.StatusCode}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a village first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void SelectImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true) // This line opens the dialog and waits for a file to be selected
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                // Konversi BitmapImage ke byte array
                byte[] imageData = ImageToByteArray(bitmap);

                // Simpan ke database
                SaveImageToDatabase(imageData);

                // Tampilkan gambar di antarmuka pengguna
                ProfileImage.Source = bitmap;
            }
        }
        private byte[] ImageToByteArray (BitmapImage bitmap)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }   

        private void SaveImageToDatabase(byte[] imageData)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string InsertQuery = "UPDATE Users SET UserImage = @UserImage WHERE User_ID = @User_ID";
                using (SqlCommand cmd4 = new SqlCommand(InsertQuery, sqlCon))
                {
                    cmd4.Parameters.AddWithValue("@UserImage", imageData);
                    cmd4.Parameters.AddWithValue("@User_ID", User_ID);
                    cmd4.ExecuteNonQuery();
                }
            }
        }
        private BitmapImage ByteArrayToImage(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();
            }
            return bitmap;
        }
        private BitmapImage GetImageFromDatabase()
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string selectQuery = "SELECT (*) FROM Users WHERE User_ID = @User_ID";
                using (SqlCommand cmd = new SqlCommand(selectQuery, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                    byte[] imageData = (byte[])cmd.ExecuteScalar();
                    return ByteArrayToImage(imageData);
                }
            }
        }
    }
}

public class Province
{
    public string Id { get; set; }
    public string Name { get; set; }

    public override string ToString() { return Name;}
}

public class Regency
{
    public string Id { get; set; }
    public string Name { get; set; }
    public override string ToString() { return Name; }
}

public class District
{
    public string Id { get; set; }
    public string Name { get; set; }
    public override string ToString() { return Name; }
}

public class Village
{
    public string Id { get; set; }
    public string Name { get; set; }
    public override string ToString() { return Name; }
}