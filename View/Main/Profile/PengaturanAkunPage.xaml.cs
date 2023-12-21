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
using System.Windows.Input;
using siKecil.View.UserEnter;
using siKecil.View;

namespace siKecil.View.Main.Profile
{
    public partial class PengaturanAkunPage : Page
    {
        private string User_ID;
        private ImageDisplay imageDisplay;

        public PengaturanAkunPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            GetDataFromSQL(User_ID);
            imageDisplay = new ImageDisplay();
            ProfileImage.Source = imageDisplay.DisplayImage(User_ID);
        }

        private void GetDataFromSQL(string User_ID)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                    string selectQuery = $"SELECT EmailAddress, Password, Username FROM Users WHERE User_Id = {User_ID}";

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
                                string editUsername = reader["Username"].ToString();
                                txtUsername.Text = editUsername;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting data from SQL: {ex.Message}");
                }
            }
        }

        private void UpdateImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));

                byte[] imageData = imageDisplay.ImageToByteArray(bitmap);

                imageDisplay.SaveImageToDatabase(User_ID, imageData);
                imageDisplay.DisplayImage(User_ID);
            }
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Anda yakin ingin keluar?", "Konfirmasi Keluar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                UserEnterView login = new UserEnterView();
                login.Show();

                Window currentMainWindow = Window.GetWindow(this);
                currentMainWindow.Close();
            }
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Anda yakin ingin menghapus akun?", "Konfirmasi Hapus Akun", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Overlay.Visibility = Visibility.Visible;

                VerificationDeleteAcc deleteAcc = new VerificationDeleteAcc(User_ID);
                deleteAccFrame.NavigationService.Navigate(deleteAcc);
            }
        }

        private void deleteAcc_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}
