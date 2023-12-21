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
    public partial class ProfileAnakPage : Page
    {
        private string User_ID;
        public ProfileAnakPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
        }

        private void JenisKelaminAnakComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JenisKelaminAnakComboBox.SelectedItem != null)
            {
                string selectedOption = ((ComboBoxItem)JenisKelaminAnakComboBox.SelectedItem).Content.ToString();
            }
        }
    }
}