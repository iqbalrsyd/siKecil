using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using siKecil.Infrastructure;

namespace siKecil.View.Main.Profile
{
    public partial class ProfileAnakPage : Page
    {
        private string User_ID;
        public ProfileAnakPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;

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

                    string selectQuery = $"SELECT NamaAnak, NamaPanggilanAnak, TanggalLahirAnak, JenisKelaminAnak FROM ProfileAnak WHERE User_Id = {User_ID}";

                    using (SqlCommand cmd = new SqlCommand(selectQuery, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string editNamaAnak = reader["NamaAnak"].ToString();
                                txtEditNamaAnak.Text = editNamaAnak;
                                string editNamaPanggilanAnak = reader["NamaPanggilanAnak"].ToString();
                                txtEditNamaPanggilanAnak.Text = editNamaPanggilanAnak;
                                string editTanggalLahirAnak = reader["TanggalLahirAnak"].ToString();
                                DateTanggalLahirAnak.Text = editTanggalLahirAnak;
                                string editJenisKelaminAnak = reader["JenisKelaminAnak"].ToString();
                                SetComboBoxSelectedItem(JenisKelaminAnakComboBox, editJenisKelaminAnak);
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

        private void SimpanProfileAnak_Click(object sender, RoutedEventArgs e)
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
                        string checkQuery = "SELECT COUNT(*) FROM ProfileAnak WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlCon, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@User_ID", User_ID);
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count > 0)
                            {
                                string updateQuery = "UPDATE ProfileAnak SET TanggalLahirAnak=@TanggalLahirAnak, NamaAnak=@NamaAnak, NamaPanggilanAnak=@NamaPanggilanAnak, JenisKelaminAnak=@JenisKelaminAnak WHERE User_Id = @User_ID";
                                using (SqlCommand cmd = new SqlCommand(updateQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.SelectedDate ?? DateTime.Now);
                                    cmd.Parameters.AddWithValue("@NamaAnak", txtEditNamaAnak.Text);
                                    cmd.Parameters.AddWithValue("@NamaPanggilanAnak", txtEditNamaPanggilanAnak.Text);
                                    cmd.Parameters.AddWithValue("@JenisKelaminAnak", JenisKelaminAnakComboBox.Text);
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = "INSERT INTO ProfileAnak (User_Id, TanggalLahirAnak, NamaAnak, NamaPanggilanAnak, JenisKelaminAnak) " +
                                                      "VALUES (@User_ID, @TanggalLahirAnak, @NamaAnak, @NamaPanggilanAnak, @JenisKelaminAnak)";
                                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.Parameters.AddWithValue("@TanggalLahirAnak", DateTanggalLahirAnak.SelectedDate ?? DateTime.Now);
                                    cmd.Parameters.AddWithValue("@NamaAnak", txtEditNamaAnak.Text);
                                    cmd.Parameters.AddWithValue("@NamaPanggilanAnak", txtEditNamaPanggilanAnak.Text);
                                    cmd.Parameters.AddWithValue("@JenisKelaminAnak", JenisKelaminAnakComboBox.Text);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();

                        NavigationService?.Navigate(new PengaturanAkunPage(User_ID));
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }
    }
}