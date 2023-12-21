using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using siKecil.Infrastructure;
using siKecil.View.Main.Profile;
using siKecil.View.UserEnter;

namespace siKecil.View
{
    public partial class VerificationDeleteAcc : Page
    {
        private readonly string User_ID;
        public VerificationDeleteAcc(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool isCheckBoxChecked = (sender as CheckBox)?.IsChecked ?? false;

            ConfirmDeleteAcc.IsEnabled = isCheckBoxChecked;
        }

        private void ConfirmDeleteAcc_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteConfirmationCheckBox.IsChecked == true)
            {
                try
                {
                    Connection connectionHelper = new Connection();

                    using (SqlConnection sqlCon = connectionHelper.GetConn())
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        string queryCount = "SELECT * FROM Users WHERE EmailAddress=@EmailAddress AND Password=@Password";
                        using (SqlCommand sqlCmdCount = new SqlCommand(queryCount, sqlCon))
                        {
                            sqlCmdCount.Parameters.AddWithValue("@EmailAddress", txtEmailAddress.Text);
                            sqlCmdCount.Parameters.AddWithValue("@Password", txtPassword.Password);

                            int userCount = Convert.ToInt32(sqlCmdCount.ExecuteScalar());

                            if (userCount > 0)
                            {
                                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                                {
                                    try
                                    {
                                        using (SqlCommand cmd1 = new SqlCommand($"DELETE FROM Users WHERE User_Id = {User_ID}; DELETE FROM ProfileOrangTua WHERE User_Id = {User_ID};" +
                                            $" DELETE FROM ProfileAnak WHERE User_Id = {User_ID}; DELETE FROM KesehatanAnak WHERE User_Id = {User_ID};" +
                                            $" DELETE FROM Chat WHERE User_Id = {User_ID}; DELETE FROM CatatTumbuhAnak WHERE User_Id = {User_ID}", sqlCon, transaction))
                                        {
                                            cmd1.Parameters.AddWithValue("@User_ID", User_ID);
                                            cmd1.ExecuteNonQuery();
                                        }

                                        transaction.Commit();

                                        UserEnterView login = new UserEnterView();
                                        login.Show();

                                        NavigationService?.GoBack();
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show($"Error deleting account: {ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Email atau kata sandi tidak valid. Silakan coba lagi.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Anda harus mengonfirmasi penghapusan akun dengan mencentang checkbox.");
            }
        }

        private void ClosePage()
        {
            // Mengakses frame dari XAML menggunakan FindName
            Frame profileFrame = (Frame)FindName("profileFrame");

            // Pastikan frame tidak null sebelum digunakan
            if (profileFrame != null && profileFrame.NavigationService.CanGoBack)
            {
                // Memanggil GoBack() jika bisa
                profileFrame.NavigationService.GoBack();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}