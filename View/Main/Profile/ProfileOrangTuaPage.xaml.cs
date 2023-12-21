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
using siKecil.Infrastructure;
using siKecil.View.Main;

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
    }
}