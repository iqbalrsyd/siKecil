using System;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using siKecil.Infrastructure;

namespace siKecil.View.UserEnter
{
    public partial class SignupPage : Page
    {
        private string User_ID;
        public static string to;

        public SignupPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void ToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsValidUserInput())
                {
                    GenerateOtpPage generateOtpPage = new GenerateOtpPage(txtEmailAddress.Text);
                    OtpFrame.NavigationService.Navigate(generateOtpPage);
                    generateOtpPage.SuccessfulOtpVerification += OnSuccessfulOtpVerificationForSignup;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnSuccessfulOtpVerificationForSignup(string emailAddress)
        {
            try
            {
                Connection connectionHelper = new Connection();

                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        String query = "INSERT INTO Users (User_ID, Username, FirstName, LastName, EmailAddress, Password) VALUES (@User_ID, @Username, @FirstName, @LastName, @EmailAddress, @Password)";
                        
                        Random random = new Random();
                        int randomNumber = random.Next(1000, 9999);
                        string User_ID = randomNumber.ToString().PadLeft(10, '0');
                        
                        SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@User_ID", User_ID);
                        sqlcmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        sqlcmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        sqlcmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        sqlcmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
                        sqlcmd.Parameters.AddWithValue("@Password", txtPassword.Password);

                        sqlcmd.ExecuteNonQuery();

                        MessageBox.Show("Sign Up Successful");
                        ((UserEnterView)Application.Current.MainWindow).NavigateToLoginPage();
                        this.Visibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsValidUserInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtEmailAddress.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Please fill in all required fields.");
                return false;
            }

            // Periksa apakah email memiliki format yang benar
            if (!IsValidEmail(txtEmailAddress.Text))
            {
                MessageBox.Show("Invalid email address format.");
                return false;
            }

            // Periksa apakah email sudah terdaftar
            if (IsEmailRegistered(txtEmailAddress.Text))
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
            // Lakukan pengecekan di database
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
    }
}