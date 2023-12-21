using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Net.Http;
using siKecil.Model;
using siKecil.Infrastructure;

namespace siKecil.View.Main.Profile
{
    public partial class ProfileOrangTuaPage : Page
    {
        private string User_ID;
        private LocationData selectedLocation = new LocationData();

        public ProfileOrangTuaPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            LoadProvincesAsync();
            GetDataFromSQL(User_ID);
        }

        private void GetDataFromSQL(string User_ID)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string selectQuery = $"SELECT TanggalLahir, Alamat, JenisKelamin, NomorTelepon, HubunganDenganAnak, AlamatKelurahan," +
                        $" AlamatKecamatan, AlamatKabKota, AlamatProvinsi FROM ProfileOrangTua WHERE User_Id = {User_ID}";
                    using (SqlCommand select = new SqlCommand(selectQuery, sqlCon))
                    {
                        using (SqlDataReader reader = select.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string editTanggalLahir = reader["TanggalLahir"].ToString();
                                DateTanggalLahirOrangTua.Text = editTanggalLahir;
                                string editAlamat = reader["Alamat"].ToString();
                                txtEditAlamat.Text = editAlamat;
                                string editJenisKelamin = reader["JenisKelamin"].ToString();
                                SetComboBoxSelectedItem(JenisKelaminOrangTuaComboBox, editJenisKelamin);
                                string editNomorTelepon = reader["NomorTelepon"].ToString();
                                txtEditNomorTelepon.Text = editNomorTelepon;
                                string editHubunganDenganAnak = reader["HubunganDenganAnak"].ToString();
                                SetComboBoxSelectedItem(HubunganDenganAnakComboBox, editHubunganDenganAnak);
                                string editAlamatProvinsi = reader["AlamatProvinsi"].ToString();
                                SetComboBoxSelectedItem(ProvinsiComboBox, editAlamatProvinsi);
                                string editAlamatKabKota = reader["AlamatKabKota"].ToString();
                                SetComboBoxSelectedItem(KabKotaComboBox, editAlamatKabKota);
                                string editAlamatKecamatan = reader["AlamatKecamatan"].ToString();
                                SetComboBoxSelectedItem(KecamatanComboBox, editAlamatKecamatan);                                
                                string editAlamatKelurahan = reader["AlamatKelurahan"].ToString();
                                SetComboBoxSelectedItem(KelurahanComboBox, editAlamatKelurahan);
                            }
                        }
                    }

                    string selectQuery2 = $"SELECT FirstName, LastName FROM Users WHERE User_Id = {User_ID}";

                    using (SqlCommand cmd = new SqlCommand(selectQuery2, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string editFirstName = reader["FirstName"].ToString();
                                txtEditFirstName.Text = editFirstName;
                                string editLastName = reader["LastName"].ToString();
                                txtEditLastName.Text = editLastName;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error getting data from SQL: {ex.Message}");
                }
            }
        }

        private void SetComboBoxSelectedItem(ComboBox comboBox, string value)
        {
            foreach (object item in comboBox.Items)
            {
                if (item is ComboBoxItem comboBoxItem && comboBoxItem.Content.ToString() == value)
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
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

        private void SimpanProfileOrangTua_Click(object sender, RoutedEventArgs e)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        string checkQuery = "SELECT COUNT(*) FROM ProfileOrangTua WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlCon, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@User_ID", User_ID);
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                string updateQuery = "UPDATE ProfileOrangTua SET TanggalLahir=@TanggalLahir, Alamat=@Alamat, JenisKelamin=@JenisKelamin, NomorTelepon=@NomorTelepon, " +
                                                      "HubunganDenganAnak=@HubunganDenganAnak, AlamatKelurahan=@AlamatKelurahan, AlamatKecamatan=@AlamatKecamatan, AlamatKabKota=@AlamatKabKota," +
                                                      "AlamatProvinsi=@AlamatProvinsi WHERE User_Id = @User_ID";
                                using (SqlCommand cmd = new SqlCommand(updateQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.Parameters.AddWithValue("@AlamatKelurahan", selectedLocation.Village);
                                    cmd.Parameters.AddWithValue("@AlamatKecamatan", selectedLocation.District);
                                    cmd.Parameters.AddWithValue("@AlamatKabKota", selectedLocation.Regency);
                                    cmd.Parameters.AddWithValue("@AlamatProvinsi", selectedLocation.Province);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = "INSERT INTO ProfileOrangTua (User_Id, TanggalLahir, Alamat, JenisKelamin, NomorTelepon, HubunganDenganAnak, AlamatKelurahan, AlamatKecamatan, AlamatKabKota,AlamatProvinsi) " +
                                                      "VALUES (@User_ID, @TanggalLahir, @Alamat, @JenisKelamin, @NomorTelepon, @HubunganDenganAnak, @AlamatKelurahan, @AlamatKecamatan, @AlamatKabKota, @AlamatProvinsi)";
                                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.Parameters.AddWithValue("@TanggalLahir", DateTanggalLahirOrangTua.DisplayDate);
                                    cmd.Parameters.AddWithValue("@Alamat", txtEditAlamat.Text);
                                    cmd.Parameters.AddWithValue("@JenisKelamin", JenisKelaminOrangTuaComboBox.Text);
                                    cmd.Parameters.AddWithValue("@NomorTelepon", txtEditNomorTelepon.Text);
                                    cmd.Parameters.AddWithValue("@HubunganDenganAnak", HubunganDenganAnakComboBox.Text);
                                    cmd.Parameters.AddWithValue("@AlamatKelurahan", selectedLocation.Village);
                                    cmd.Parameters.AddWithValue("@AlamatKecamatan", selectedLocation.District);
                                    cmd.Parameters.AddWithValue("@AlamatKabKota", selectedLocation.Regency);
                                    cmd.Parameters.AddWithValue("@AlamatProvinsi", selectedLocation.Province);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();

                        NavigationService?.Navigate(new ProfileAnakPage(User_ID));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
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
                            selectedLocation.Village = KelurahanComboBox.SelectedValue.ToString();
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
    }
}