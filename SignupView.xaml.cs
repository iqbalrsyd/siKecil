using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace siKecil
{
    public partial class SignupView : Window
    {
        private string User_ID;
        string randomCodeOTP;
        public static string to;

        public SignupView()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string from, pass, messageBody;
                Random rand = new Random();
                randomCodeOTP = (rand.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                to = (txtEmailAddress.Text).ToString();

                string domain = to.Split('@')[1].ToLower();

                if (domain.Contains("gmail.com"))
                {
                    from = "rasyad3003@gmail.com";
                    pass = "ymvn frzk ejbw ihwv";
                    messageBody = "Your OTP code is " + randomCodeOTP;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "Password OTP Code Application siKecil";

                    smtp.Send(message);

                    MessageBox.Show("Code Sent Successfully");
                    otpStackPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Unsupported email domain");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void SubmitOtp_Click(object sender, RoutedEventArgs e)
        {
            if (randomCodeOTP == (txtOtp.Text).ToString())
            {
                to = txtEmailAddress.Text;
                Connection connectionHelper = new Connection();

                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        String query = "INSERT INTO Users (User_ID, FirstName, LastName, EmailAddress, Password) VALUES (@User_ID, @FirstName, @LastName, @EmailAddress, @Password)";

                        Random random = new Random();

                        int randomNumber = random.Next(1000, 9999);

                        User_ID = randomNumber.ToString().PadLeft(10, '0');

                        SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                        sqlcmd.CommandType = CommandType.Text;
                        sqlcmd.Parameters.AddWithValue("@User_ID", User_ID);
                        sqlcmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        sqlcmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        sqlcmd.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
                        sqlcmd.Parameters.AddWithValue("@Password", txtPassword.Password);

                        sqlcmd.ExecuteNonQuery();

                        MessageBox.Show("Sign Up Successful");
                        LoginView login = new LoginView(User_ID);
                        login.Show();
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Wrong Code");
            }
        }
        private void ToLoginViewLabel(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView(User_ID);
            login.Show();
            this.Close();
        }

        private void txtOtp_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}