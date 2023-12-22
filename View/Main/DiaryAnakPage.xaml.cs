using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Runtime.InteropServices;
using siKecil.View.UserEnter;

namespace siKecil.View.Main
{
    /// <summary>
    /// Interaction logic for DiaryAnakPage.xaml
    /// </summary>
    public partial class DiaryAnakPage : Page
    {
        private readonly string User_ID;
        Connection connectionHelper = new Connection();
        private Person person;
        public DiaryAnakPage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            Loaded += DiaryAnakPage_Loaded;

            person = GetPersonFromDatabase();
            DataContext = person;
            UpdateAge();
            NamaAnak();
        }

        private void DiaryAnakPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid(User_ID);
            LoadRiwayatKesehatan(User_ID);
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
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlCon, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@User_ID", User_ID);
                            String query = "INSERT INTO CatatTumbuhAnak (User_ID, No, Tanggal, Berat, Tinggi, LingkarKepala, [Status Gizi], [Status LK]) VALUES (@User_ID, (SELECT ISNULL(MAX(No), 0) + 1 FROM CatatTumbuhAnak WHERE User_ID = @User_ID), @Tanggal, @Berat, @Tinggi, @LingkarKepala, @StatusGizi, @StatusLK)";
                            using (SqlCommand cmd = new SqlCommand(query, sqlCon, transaction))
                            {
                                double weight;
                                if (!double.TryParse(txtBerat.Text, out weight))
                                {
                                    MessageBox.Show("Please enter a valid weight.");
                                    return;
                                }

                                double height;
                                if (!double.TryParse(txtTinggi.Text, out height))
                                {
                                    MessageBox.Show("Please enter a valid height.");
                                    return;
                                }

                                double lingkarKepala;
                                if (!double.TryParse(txtLingkarKepala.Text, out lingkarKepala))
                                {
                                    MessageBox.Show("Please enter a valid lingkarKepala.");
                                    return;
                                }

                                string gender = person.JenisKelaminAnak;
                                int age = CalculateAge(person.TanggalLahirAnak, DateTime.Today);
                                Criteria criteria = new Criteria(gender, weight, height, age, lingkarKepala);
                                string statusGizi = criteria.CalculateGizi();
                                string statusLK = criteria.CalculateLK();

                                cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                cmd.Parameters.AddWithValue("@Tanggal", datePicker.SelectedDate ?? DateTime.Now);
                                cmd.Parameters.AddWithValue("@Berat", txtBerat.Text);
                                cmd.Parameters.AddWithValue("@Tinggi", txtTinggi.Text);
                                cmd.Parameters.AddWithValue("@LingkarKepala", txtLingkarKepala.Text);
                                cmd.Parameters.AddWithValue("@StatusGizi", statusGizi);
                                cmd.Parameters.AddWithValue("@StatusLK", statusLK);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Data berhasil disimpan");
                        LoadDataGrid(User_ID);

                        datePicker.Text = "";
                        txtBerat.Text = "";
                        txtTinggi.Text = "";
                        txtLingkarKepala.Text = "";
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

                    string showQuery = $"SELECT No, Tanggal, Berat, Tinggi, LingkarKepala, [Status Gizi], [Status LK] FROM CatatTumbuhAnak WHERE User_Id = {User_ID}";
                    using (SqlCommand cmd = new SqlCommand(showQuery, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);
                                dataTable.DefaultView.Sort = "No ASC";
                                dataGrid.ItemsSource = dataTable.DefaultView;
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

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            SimpanData.Visibility = Visibility.Collapsed;
            UpdateData.Visibility = Visibility.Visible;
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

                    string updateQuery = "UPDATE CatatTumbuhAnak SET Tanggal = @Tanggal, Berat = @Berat, Tinggi = @Tinggi, LingkarKepala = @LingkarKepala, [Status Gizi] = @StatusGizi, [Status LK] = @StatusLK WHERE No = @No AND User_Id = @User_Id";
                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlCon))
                    {
                        double weight;
                        if (!double.TryParse(txtBerat.Text, out weight))
                        {
                            MessageBox.Show("Please enter a valid weight.");
                            return;
                        }

                        double height;
                        if (!double.TryParse(txtTinggi.Text, out height))
                        {
                            MessageBox.Show("Please enter a valid height.");
                            return;
                        }

                        double lingkarKepala;
                        if (!double.TryParse(txtLingkarKepala.Text, out lingkarKepala))
                        {
                            MessageBox.Show("Please enter a valid lingkarKepala.");
                            return;
                        }

                        string gender = person.JenisKelaminAnak;
                        int age = CalculateAge(person.TanggalLahirAnak, DateTime.Today);
                        Criteria criteria = new Criteria(gender, weight, height, age, lingkarKepala);
                        string statusGizi = criteria.CalculateGizi();
                        string statusLK = criteria.CalculateLK();

                        updateCommand.Parameters.AddWithValue("@Tanggal", datePicker.SelectedDate ?? DateTime.Now);
                        updateCommand.Parameters.AddWithValue("@Berat", txtBerat.Text);
                        updateCommand.Parameters.AddWithValue("@Tinggi", txtTinggi.Text);
                        updateCommand.Parameters.AddWithValue("@LingkarKepala", txtLingkarKepala.Text);
                        updateCommand.Parameters.AddWithValue("@User_ID", User_ID);
                        updateCommand.Parameters.AddWithValue("@No", selectedNo);
                        updateCommand.Parameters.AddWithValue("@StatusGizi", statusGizi);
                        updateCommand.Parameters.AddWithValue("@StatusLK", statusLK);
                        updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil di update!");
                        SimpanData.Visibility = Visibility.Visible;
                        UpdateData.Visibility = Visibility.Collapsed;

                        LoadDataGrid(User_ID);
                    }
                }

                datePicker.Text = "";
                txtBerat.Text = "";
                txtTinggi.Text = "";
                txtLingkarKepala.Text = "";
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Anda yakin ingin menghapus data?", "Konfirmasi hapus data", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (SqlConnection sqlCon = connectionHelper.GetConn())
                    {
                        sqlCon.Open();

                        string selectedNo = selectedRow["No"].ToString();

                        string deleteQuery = "DELETE FROM CatatTumbuhAnak WHERE No = @No AND User_Id = @User_ID";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlCon))
                        {
                            deleteCommand.Parameters.AddWithValue("@No", int.Parse(selectedNo));
                            deleteCommand.Parameters.AddWithValue("@User_ID", User_ID);
                            deleteCommand.ExecuteNonQuery();
                            MessageBox.Show("Data berhasil dihapus!");
                        }

                        string updateQuery = "UPDATE CatatTumbuhAnak SET No = No - 1 WHERE User_ID = @User_ID AND No > @No";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlCon))
                        {
                            updateCommand.Parameters.AddWithValue("@User_ID", User_ID);
                            updateCommand.Parameters.AddWithValue("@No", int.Parse(selectedNo));
                            updateCommand.ExecuteNonQuery();
                        }
                        LoadDataGrid(User_ID);
                    }
                }
            }
        }

        private Person GetPersonFromDatabase()
        {
            Person result = new Person();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string user_id = this.User_ID;
                string getPerson = "SELECT NamaPanggilanAnak, TanggalLahirAnak, JenisKelaminAnak FROM ProfileAnak WHERE User_ID = @User_ID";

                using (SqlCommand command = new SqlCommand(getPerson, sqlCon))
                {
                    command.Parameters.AddWithValue("@User_ID", user_id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result.NamaAnak = reader.GetString(0);
                            result.TanggalLahirAnak = reader.GetDateTime(1);
                            result.JenisKelaminAnak = reader.GetString(2);
                        }
                    }
                }
            }
            return result;
        }
        private void UpdateAge()
        {
            int age = CalculateAge(person.TanggalLahirAnak, DateTime.Today);
            string ageChild = age.ToString();
            DateTime tanggal = person.TanggalLahirAnak;
            if(tanggal == null)
            {
                AgeTextBox.Text = "[Nama Anak]";
            }
            else
            {
                AgeTextBox.Text = ageChild;
            }
        }
        private void BirthDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateAge();
        }

        private void NamaAnak()
        {
            string panggilanAnak = person.NamaAnak;
            NamaAnakText.Text = panggilanAnak != null ? panggilanAnak : "[nama anak]";
        }

        private int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            int ageInMonths = (currentDate.Year - birthDate.Year) * 12 + currentDate.Month - birthDate.Month;
            if (currentDate.Day < birthDate.Day)
            {
                ageInMonths--;
            }
            return ageInMonths;
        }

        private void checkStatusGizi()
        {
            double weight;
            if (!double.TryParse(txtBerat.Text, out weight))
            {
                MessageBox.Show("Please enter a valid weight.");
                return;
            }

            double height;
            if (!double.TryParse(txtTinggi.Text, out height))
            {
                MessageBox.Show("Please enter a valid height.");
                return;
            }
            double lingkarKepala;
            if (!double.TryParse(txtLingkarKepala.Text, out lingkarKepala))
            {
                MessageBox.Show("Please enter a valid lingkarKepala.");
                return;
            }
            string gender = person.JenisKelaminAnak;
            int age = CalculateAge(person.TanggalLahirAnak, DateTime.Today);
            Criteria criteria = new Criteria(gender, weight, height, age, lingkarKepala);
            string statusGizi = criteria.CalculateGizi();
            string statusLK = criteria.CalculateLK();

            MessageBox.Show("Status Kesehatan Anak\n" + "Status Gizi: " + statusGizi + "\nStatus LK: " + statusLK);
        }

        private void cekStatusButton(object sender, RoutedEventArgs e)
        {
            checkStatusGizi();
        }

        private void simpanKesehatanAnak_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                using (SqlTransaction transaction = sqlCon.BeginTransaction())
                {
                    try
                    {
                        string checkQuery = "SELECT COUNT(*) FROM KesehatanAnak WHERE User_Id = @User_ID";
                        using (SqlCommand checkCmd = new SqlCommand(checkQuery, sqlCon, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@User_ID", User_ID);
                            int count2 = (int)checkCmd.ExecuteScalar();

                            if (count2 > 0)
                            {
                                string updateQuery = "UPDATE KesehatanAnak SET RiwayatAlergi=@RiwayatAlergi, RiwayatPenyakit=@RiwayatPenyakit, GolonganDarah=@GolonganDarah WHERE User_Id = @User_ID";
                                using (SqlCommand cmd = new SqlCommand(updateQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@RiwayatAlergi", txtRiwayatAlergi.Text);
                                    cmd.Parameters.AddWithValue("@RiwayatPenyakit", txtRiwayatPenyakit.Text);
                                    cmd.Parameters.AddWithValue("@GolonganDarah", GolonganDarahComboBox.Text);
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = "INSERT INTO KesehatanAnak (User_Id, RiwayatAlergi, RiwayatPenyakit, GolonganDarah) " +
                                                      "VALUES (@User_ID, @RiwayatAlergi, @RiwayatPenyakit, @GolonganDarah)";
                                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlCon, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                                    cmd.Parameters.AddWithValue("@RiwayatAlergi", txtRiwayatAlergi.Text);
                                    cmd.Parameters.AddWithValue("@RiwayatPenyakit", txtRiwayatPenyakit.Text);
                                    cmd.Parameters.AddWithValue("@GolonganDarah", GolonganDarahComboBox.Text);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        transaction.Commit();
                        MessageBox.Show("Riwayat kesehatan anak berhasil disimpan!");
                        SimpanKesehatanAnak.Visibility = Visibility.Collapsed;
                        EditKesehatanAnak.Visibility = Visibility.Visible;
                        LoadRiwayatKesehatan(User_ID);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving changes: " + ex.Message);
                    }
                }
            }
        }

        private void editKesehatanAnak_Click(object sender, RoutedEventArgs e)
        {
            SimpanKesehatanAnak.Visibility = Visibility.Visible;
            EditKesehatanAnak.Visibility = Visibility.Collapsed;

            txtRiwayatAlergi.IsEnabled = true;
            txtRiwayatPenyakit.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GolonganDarahComboBox.SelectedItem != null)
            {
                string selectedOption = ((ComboBoxItem)GolonganDarahComboBox.SelectedItem).Content.ToString();
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
        private void LoadRiwayatKesehatan(string User_ID)
        {
            try
            {
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string showQuery = $"SELECT RiwayatAlergi, RiwayatPenyakit, GolonganDarah FROM KesehatanAnak WHERE User_Id = {User_ID}";
                    using (SqlCommand cmd = new SqlCommand(showQuery, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string riwayatAlergi = reader["RiwayatAlergi"].ToString();
                                txtRiwayatAlergi.Text = riwayatAlergi;
                                string riwayatPenyakit = reader["RiwayatPenyakit"].ToString();
                                txtRiwayatPenyakit.Text = riwayatPenyakit;
                                string selectedGoldar = reader["GolonganDarah"].ToString();
                                SetComboBoxSelectedItem(GolonganDarahComboBox, selectedGoldar);
                            }
                        }
                    }
                }
                txtRiwayatAlergi.IsEnabled = false;
                txtRiwayatPenyakit.IsEnabled = false;   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        
    }

    public class Person
    {
        public string NamaAnak { get; set; }
        public DateTime TanggalLahirAnak { get; set; }
        public string JenisKelaminAnak { get; set; }
    }
}

