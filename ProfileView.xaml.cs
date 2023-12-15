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
using siKecil.Model;
using System.ComponentModel;

namespace siKecil
{
    public partial class ProfileView : Window
    {
        private readonly string User_ID;
        private LocationData selectedLocation = new LocationData();
        private ProfileDataModel profileDataModel;

        public ProfileView(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            Loaded += Profile_Loaded;
        }

        private void Profile_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            DisplayImage();
            LoadProvincesAsync();
            GetDataFromSQL(User_ID);
        }

        private ProfileDataModel GetDataFromSQL(string User_ID)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                    string selectQuery = $"SELECT EmailAddress, Password FROM Users WHERE User_Id = {User_ID}";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string editEmail = reader["EmailAddress"].ToString();
                                txtEditEmailAddress.Text = editEmail;
                                string editPassword = reader["Password"].ToString();
                                txtEditPassword.Password = editPassword;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting data from SQL: {ex.Message}");
                }
            }


            return profileDataModel;
        }

        private void EditProfileButton(object sender, RoutedEventArgs e)
        {
            Connection connectionHelper = new Connection();
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
                            cmd1.Parameters.AddWithValue("@EmailAddress", string.IsNullOrEmpty(txtEditEmailAddress.Text) ? null : txtEditEmailAddress.Text);
                            cmd1.Parameters.AddWithValue("@Password", string.IsNullOrEmpty(txtEditPassword.Password) ? null : txtEditPassword.Password);
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
                                                      "HubunganDenganAnak=@HubunganDenganAnak, AlamatKelurahan=@AlamatKelurahan, AlamatKecamatan=@AlamatKecamatan, AlamatKabKota=@AlamatKabKota," +
                                                      "AlamatProvinsi=@AlamatProvinsi WHERE User_Id = @User_ID";
                                using (SqlCommand cmd2 = new SqlCommand(updateQuery2, sqlCon, transaction))
                                {
                                    cmd2.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd2.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd2.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd2.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd2.Parameters.AddWithValue("@AlamatKelurahan", selectedLocation.Village);
                                    cmd2.Parameters.AddWithValue("@AlamatKecamatan", selectedLocation.District);
                                    cmd2.Parameters.AddWithValue("@AlamatKabKota", selectedLocation.Regency);
                                    cmd2.Parameters.AddWithValue("@AlamatProvinsi", selectedLocation.Province);

                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery2 = "INSERT INTO ProfileOrangTua (User_Id, TanggalLahir, Alamat, JenisKelamin, NomorTelepon, HubunganDenganAnak, AlamatKelurahan, AlamatKecamatan, AlamatKabKota,AlamatProvinsi) " +
                                                      "VALUES (@User_ID, @TanggalLahir, @Alamat, @JenisKelamin, @NomorTelepon, @HubunganDenganAnak, @AlamatKelurahan, @AlamatKecamatan, @AlamatKabKota, @AlamatProvinsi)";
                                using (SqlCommand cmd2 = new SqlCommand(insertQuery2, sqlCon, transaction))
                                {
                                    cmd2.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd2.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd2.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd2.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd2.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd2.Parameters.AddWithValue("@AlamatKelurahan", selectedLocation.Village);
                                    cmd2.Parameters.AddWithValue("@AlamatKecamatan", selectedLocation.District);
                                    cmd2.Parameters.AddWithValue("@AlamatKabKota", selectedLocation.Regency);
                                    cmd2.Parameters.AddWithValue("@AlamatProvinsi", selectedLocation.Province);
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
                                    cmd3.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.SelectedDate ?? DateTime.Now);
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
                                    cmd3.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.SelectedDate ?? DateTime.Now);
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
                            selectedLocation.Village = selectedVillage.Name;
                        }

                        if (selectedVillage != null)
                        {
                            selectedLocation.Province = ProvinsiComboBox.SelectedValue.ToString();
                            selectedLocation.Regency = KabKotaComboBox.SelectedValue.ToString();
                            selectedLocation.District = KecamatanComboBox.SelectedValue.ToString();
                            selectedLocation.Village = selectedVillage.Name;
                        }
                        else
                        {
                            MessageBox.Show("Please select a village first.");
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

        private BitmapImage GetImageFromDatabase()
        {
            try
            {
                Connection connectionHelper = new Connection();
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string selectQuery = "SELECT UserImage FROM Users WHERE User_ID = @User_ID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@User_ID", User_ID);
                        byte[] imageData = (byte[])cmd.ExecuteScalar();
                        return ByteArrayToImage(imageData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting image from database: {ex.Message}");
                return null;
            }
        }

        private void SelectImage_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                byte[] imageData = ImageToByteArray(bitmap);

                SaveImageToDatabase(imageData);

                ProfileImage.Source = bitmap;
            }
        }
        private byte[] ImageToByteArray(BitmapImage bitmap)
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
            Connection connectionHelper = new Connection();
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

        private void DisplayImage()
        {
            BitmapImage image = GetImageFromDatabase();
            if (image != null)
            {
                ProfileImage.Source = image;
            }
        }
    }
}