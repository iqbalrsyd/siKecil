using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;

namespace siKecil
{
    /// <summary>
    /// Interaction logic for loginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private string User_ID;
        public LoginView(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            {
                Connection connectionHelper = new Connection();

                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        string queryCount = "SELECT COUNT(*) FROM Users WHERE EmailAddress=@EmailAddress AND Password=@Password";

                        using (SqlCommand sqlCmdCount = new SqlCommand(queryCount, sqlCon))
                        {
                            sqlCmdCount.CommandType = CommandType.Text;
                            sqlCmdCount.Parameters.AddWithValue("@EmailAddress", txtUsername.Text);
                            sqlCmdCount.Parameters.AddWithValue("@Password", txtPassword.Password);

                            int count = Convert.ToInt32(sqlCmdCount.ExecuteScalar());

                            if (count == 1)
                            {
                                string queryUserId = "SELECT User_ID FROM dbo.Users WHERE EmailAddress=@EmailAddress AND Password=@Password";

                                using (SqlCommand sqlCmdUserId = new SqlCommand(queryUserId, sqlCon))
                                {
                                    sqlCmdUserId.CommandType = CommandType.Text;
                                    sqlCmdUserId.Parameters.AddWithValue("@EmailAddress", txtUsername.Text);
                                    sqlCmdUserId.Parameters.AddWithValue("@Password", txtPassword.Password);

                                    string User_ID = sqlCmdUserId.ExecuteScalar()?.ToString();

                                    MainWindow dashboard = new MainWindow(User_ID);
                                    dashboard.Show();
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username atau Password salah!");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            SignupView form = new SignupView();
            form.Show();
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}