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
using System.Globalization;

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
            Loaded += CatatTumbuhAnak_Loaded;
        }

        private void CatatTumbuhAnak_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(User_ID);
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
                            String query = "INSERT INTO CatatTumbuhAnak (User_ID, No, Tanggal, Berat, Tinggi, LingkarKepala) VALUES (@User_ID, (SELECT ISNULL(MAX(No), 0) + 1 FROM CatatTumbuhAnak WHERE User_ID = @User_ID), @Tanggal, @Berat, @Tinggi, @LingkarKepala)";
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
                        LoadDataGrid(User_ID);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving changes: " + ex.Message);
                    }
                }
            }
        }
        
        private void LoadDataGrid(string User_ID)
        {
            try
            {
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string showQuery = $"SELECT No, Tanggal, Berat, Tinggi, LingkarKepala FROM CatatTumbuhAnak WHERE User_Id = {User_ID}";
                    using (SqlCommand cmd = new SqlCommand(showQuery, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataSet dataSet = new DataSet();
                                dataSet.Load(reader, LoadOption.OverwriteChanges, "CatatTumbuhAnak");

                                dataGrid.AutoGeneratingColumn += (sender, e) =>
                                {
                                    if (e.PropertyName == "Tanggal" && e.Column is DataGridTextColumn textColumn)
                                    {
                                        textColumn.Binding.StringFormat = "d MMMM yyyy";
                                    }
                                };

                                dataGrid.ItemsSource = dataSet.Tables["CatatTumbuhAnak"].DefaultView;
                                dataGrid.IsReadOnly = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ToDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(User_ID);
            dashboard.Show();
            this.Close();
        }

        private void editDataButton(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

                string tanggalValue = selectedRow["Tanggal"].ToString();
                string beratValue = selectedRow["Berat"].ToString();
                string tinggiValue = selectedRow["Tinggi"].ToString();
                string lingkarKepalaValue = selectedRow["LingkarKepala"].ToString();

                datePicker.Text = tanggalValue;
                txtBerat.Text = beratValue;
                txtTinggi.Text = tinggiValue;
                txtLingkarKepala.Text = lingkarKepalaValue;
            }

            else
            {
                datePicker.Text = "";
                txtBerat.Text = "";
                txtTinggi.Text = "";
                txtLingkarKepala.Text = "";
            }
        }

        private void updateData(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string selectedNo = selectedRow["No"].ToString();

                    string updateQuery = "UPDATE CatatTumbuhAnak SET Tanggal = @Tanggal, Berat = @Berat, Tinggi = @Tinggi, LingkarKepala = @LingkarKepala WHERE No = @No";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlCon))
                    {
                        
                        updateCommand.Parameters.AddWithValue("@Tanggal", datePicker.DisplayDate);
                        updateCommand.Parameters.AddWithValue("@Berat", txtBerat.Text);
                        updateCommand.Parameters.AddWithValue("@Tinggi", txtTinggi.Text);
                        updateCommand.Parameters.AddWithValue("@LingkarKepala", txtLingkarKepala.Text);
                        updateCommand.Parameters.AddWithValue("@User_ID", User_ID);
                        updateCommand.Parameters.AddWithValue("@No", selectedNo);

                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil di update!");

                        LoadDataGrid(User_ID);
                    }
                }
            }
        }
        private void deleteData(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;

                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string selectedNo = selectedRow["No"].ToString();

                    string deleteQuery = "DELETE FROM CatatTumbuhAnak WHERE No = @No";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlCon))
                    {
                        deleteCommand.Parameters.AddWithValue("@No", int.Parse(selectedNo));
                        deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil dihapus!");
                    }
                    LoadDataGrid(User_ID); 
                }
            }
        }
    }
}


