using System;
using System.Data;
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
using System.Windows.Navigation;

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
                    MessageBox.Show($"Error getting data from SQL: {ex.Message}");
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

        private void SimpanAkun_Click(object sender, RoutedEventArgs e)
        {
            string emailLama = string.Empty;
            string passwordLama = string.Empty;

            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string queryGetOldData = "SELECT EmailAddress, Password FROM Users WHERE User_ID = @User_ID";

                    using (SqlCommand cmdGetOldData = new SqlCommand(queryGetOldData, sqlCon))
                    {
                        cmdGetOldData.Parameters.AddWithValue("@User_ID", User_ID);

                        using (SqlDataReader reader = cmdGetOldData.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                emailLama = reader["EmailAddress"].ToString();
                                passwordLama = reader["Password"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting old data from database: {ex.Message}");
                }
            }

            string emailBaru = txtEditEmailAddress.Text;
            string passwordBaru = txtEditPassword.Password;

            if (emailBaru == emailLama && passwordBaru == passwordLama)
            {
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        string UpdateUsername = "UPDATE Users SET Username=@Username WHERE User_ID = @User_ID";
                        using(SqlCommand cmdUpdateUsername = new SqlCommand(UpdateUsername, sqlCon))
                        {
                            cmdUpdateUsername.Parameters.AddWithValue("@User_ID", User_ID);
                            cmdUpdateUsername.Parameters.AddWithValue("@Username", txtUsername.Text);
                            cmdUpdateUsername.ExecuteNonQuery();

                            MessageBox.Show("Penyimpanan Berhasil");

                            GetDataFromSQL(User_ID);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving changes: " + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    if (IsValidUserInput())
                    {
                        Overlay.Visibility = Visibility.Visible;

                        GenerateOtpPage generateOtpPage = new GenerateOtpPage(txtEditEmailAddress.Text);
                        OtpFrame.NavigationService.Navigate(generateOtpPage);

                        generateOtpPage.SuccessfulOtpVerification += OnSuccessfulOtpVerificationForUpdateProfile;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void OnSuccessfulOtpVerificationForUpdateProfile(string emailAddress)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();

                    string UpdateUser = "UPDATE Users SET Username=@Username, EmailAddress=@EmailAddress, Password=@Password WHERE User_ID = @User_ID";
                    using (SqlCommand cmdUpdateUser = new SqlCommand(UpdateUser, sqlCon))
                    {
                        cmdUpdateUser.Parameters.AddWithValue("@User_ID", User_ID);
                        cmdUpdateUser.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmdUpdateUser.Parameters.AddWithValue("EmailAddress", txtEditEmailAddress.Text);
                        cmdUpdateUser.Parameters.AddWithValue("Password", txtEditPassword.Password);
                        cmdUpdateUser.ExecuteNonQuery();

                        MessageBox.Show("Penyimpanan Berhasil");

                        GetDataFromSQL(User_ID);
                        this.Visibility = Visibility.Collapsed;
                        NavigationService?.Navigate(new ProfileOrangTuaPage(User_ID));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving changes: " + ex.Message);
                }
            }
        }

        private bool IsValidUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEditEmailAddress.Text) ||
                string.IsNullOrWhiteSpace(txtEditPassword.Password))
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }

            if (!IsValidEmail(txtEditEmailAddress.Text))
            {
                MessageBox.Show("Invalid email address format.");
                return false;
            }

            if (IsEmailRegistered(txtEditEmailAddress.Text))
            {
                MessageBox.Show("Email address is already registered.");
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsEmailRegistered(string email)
        {
            Connection connectionHelper = new Connection();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE EmailAddress = @EmailAddress";
                using (SqlCommand sqlCmd = new SqlCommand(query, sqlCon))
                {
                    sqlCmd.Parameters.AddWithValue("@EmailAddress", email);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }

        private void OtpFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}