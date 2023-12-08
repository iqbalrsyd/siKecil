using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace siKecil
{
    /// <summary>
    /// Interaction logic for CatatTumbuhAnak.xaml
    /// </summary>
    public partial class CatatTumbuhAnak : Window
    {
        private readonly string User_ID;
        Connection connectionHelper = new Connection();
        public CatatTumbuhAnak(string User_ID)
        { 
            InitializeComponent();
            this.User_ID = User_ID;
        }

         private void SaveCatatanTumbuhAnak(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        if (sqlCon.State == ConnectionState.Closed)
                            sqlCon.Open();

                        string checkQuery = "SELECT FROM Users WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd2 = new SqlCommand(checkQuery, sqlCon, transaction))
                        {
                            checkCmd2.Parameters.AddWithValue("@User_ID", User_ID);
                            String query = "INSERT INTO CatatTumbuhAnak (User_ID, Tanggal, Berat, Tinggi, LingkarKepala) VALUES (@User_ID, @Tanggal, @Berat, @Tinggi, @LingkarKepala)";
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon, transaction))
                            {
                                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                cmd.Parameters.AddWithValue("@Tanggal", datePicker.DisplayDate);
                                cmd.Parameters.AddWithValue("@Berat", txtBerat.Text);
                                cmd.Parameters.AddWithValue("@Tinggi", txtTinggi.Text);
                                cmd.Parameters.AddWithValue("@LingkarKepala", txtLingkarKepala.Text);                                
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Data berhasil disimpan");
                    }

                    

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving changes: " + ex.Message);
                    }
                }
            }
        }
    }
}
