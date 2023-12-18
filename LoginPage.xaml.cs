using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace siKecil
{
    public partial class LoginPage : Page
    {
        private string User_ID;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ToDashboard_Click(object sender, RoutedEventArgs e)
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
                            sqlCmdCount.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
                            sqlCmdCount.Parameters.AddWithValue("@Password", txtPassword.Password);

                            int count = Convert.ToInt32(sqlCmdCount.ExecuteScalar());

                            if (count == 1)
                            {
                                string queryUserId = "SELECT User_ID FROM dbo.Users WHERE EmailAddress=@EmailAddress AND Password=@Password";

                                using (SqlCommand sqlCmdUserId = new SqlCommand(queryUserId, sqlCon))
                                {
                                    sqlCmdUserId.CommandType = CommandType.Text;
                                    sqlCmdUserId.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
                                    sqlCmdUserId.Parameters.AddWithValue("@Password", txtPassword.Password);

                                    string User_ID = sqlCmdUserId.ExecuteScalar().ToString();

                                    MainWindow dashboard = new MainWindow(User_ID);
                                    dashboard.Show();
                                    Window currentUserEnter = Window.GetWindow(this);
                                    currentUserEnter.Close();
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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}