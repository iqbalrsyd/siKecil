using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using siKecil.Infrastructure;
using siKecil;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Globalization;

namespace siKecil.View.Main
{

    public partial class HomePage : Page
    {
        private readonly string User_ID;
        private DateTime selectedDate;
        private readonly Connection connectionHelper;

        private object NamaAnak; 
        private DateTime birthDate;

        public HomePage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            Title = "Beranda";
            connectionHelper = new Connection();

            InitializeGreetingText();
        }


        private void InitializeGreetingText()
        {
            try
            {
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    string user_id = this.User_ID;
                    string sqlQuery = $"SELECT FirstName FROM dbo.Users WHERE User_ID = '{user_id}'";

                    using (SqlCommand command = new SqlCommand(sqlQuery, sqlCon))
                    {
                        sqlCon.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstname = reader["FirstName"].ToString();
                                greetingText.Text = $"Hallo {firstname}!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendar.SelectedDate.HasValue)
            {
                selectedDate = calendar.SelectedDate.Value;
                SetImmunizationMessage(selectedDate); // Call SetImmunizationMessage when date changes
            }
            else
            {
                // Clear message jika tidak ada tanggal yang dipilih
                MessageTextBlock1.Text = "";
                MessageTextBlock2.Text = $"{selectedDate.ToString("dd/MM/yyyy")}";
                MessageTextBlock3.Text = "";
                MessageTextBlock.Text = "";
            }
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            // Tandai tanggal-tanggal yang telah dijadwalkan saat kalender dimuat
            MarkScheduledDates();
        }

        private void SaveSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    connection.Open();

                    string query = "INSERT INTO dbo.Kalender (EventTitle, EventDate, StatusCalender) VALUES (@EventTitle, @EventDate, 'Scheduled')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        string EventTitle = txtEventTitle.Text;

                        if (string.IsNullOrWhiteSpace(EventTitle))
                        {
                            MessageBox.Show("Please enter an event title.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        command.Parameters.AddWithValue("@EventTitle", EventTitle);
                        command.Parameters.AddWithValue("@EventDate", selectedDate);
                        command.Parameters.AddWithValue("@User_ID", User_ID ?? (object)DBNull.Value);


                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MarkScheduledDates();
                            MessageBox.Show($"Scheduled '{EventTitle}' on {selectedDate.ToShortDateString()}. Data saved to database.", "Schedule Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save data to database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Error occurred: {sqlEx.Message}", "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    connection.Open();
                    string deleteQuery = "DELETE FROM Kalender WHERE EventDate = @EventDate";

                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@EventDate", selectedDate);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ClearCalendarMark(selectedDate);
                            MessageBox.Show($"Data on {selectedDate.ToShortDateString()} has been deleted.", "Data Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MarkScheduledDates()
        {
            calendar.BlackoutDates.Clear();
            if (calendar.SelectedDates != null)
            {
                foreach (DateTime scheduledDate in GetScheduledDatesFromDatabase())
                {
                    if (calendar.SelectedDates.Any(d => d.Date == scheduledDate.Date))
                    {
                        CalendarDateRange dateRange = new CalendarDateRange(scheduledDate);
                        calendar.BlackoutDates.Add(dateRange);
                    }
                }
            }
        }


        private List<DateTime> GetScheduledDatesFromDatabase(SqlConnection connection)
        {
            List<DateTime> scheduledDates = new List<DateTime>();

            try
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                string query = "SELECT EventDate FROM dbo.Kalender WHERE StatusCalender = 'Scheduled'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime scheduledDate = (DateTime)reader["EventDate"];
                            scheduledDates.Add(scheduledDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return scheduledDates;
        }


        private List<DateTime> GetScheduledDatesFromDatabase()
        {
            List<DateTime> scheduledDates = new List<DateTime>();

            try
            {
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    scheduledDates = GetScheduledDatesFromDatabase(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return scheduledDates;
        }


        private void ClearCalendarMark(DateTime date)
        {
            // Tambahkan logika untuk menghapus tanda pada kalender di sini
            // Misalnya, mengembalikan warna atau menghapus ikon pada tanggal yang telah dijadwalkan
            CalendarDateRange dateRange = new CalendarDateRange(date);
            calendar.BlackoutDates.Remove(dateRange);
        }

        private void txtEventTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Implementasi logika yang diinginkan ketika teks berubah
            // Cek apakah perubahan terjadi karena tombol Enter ditekan
            if (e.Changes.Any(change => change.RemovedLength > 0 && change.AddedLength == 1 && ((TextBox)sender).Text.EndsWith("\r")))
            {
                // Lakukan sesuatu saat tombol Enter ditekan
                MessageBox.Show("Enter key pressed!", "Key Pressed", MessageBoxButton.OK, MessageBoxImage.Information);

                // Setel teks kembali tanpa karakter Enter
                ((TextBox)sender).Text = ((TextBox)sender).Text.TrimEnd('\r');
            }
            else
            {
                // Membersihkan pesan jika tidak ada teks
                if (MessageTextBlock1 != null)
                {
                    MessageTextBlock1.Text = "";
                }

                if (MessageTextBlock2 != null)
                {
                    MessageTextBlock2.Text = $"";
                }

                if (MessageTextBlock3 != null)
                {
                    MessageTextBlock3.Text = "";
                }

                if (MessageTextBlock != null)
                {
                    MessageTextBlock.Text = "";
                }
            }
        }

        private void SetImmunizationMessage(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection connection = connectionHelper.GetConn())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    string query = "SELECT NamaAnak, TanggalLahirAnak, JenisKelaminAnak FROM dbo.ProfileAnak WHERE User_ID = @User_ID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@User_ID", User_ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NamaAnak = reader["NamaAnak"];
                                birthDate = reader.GetDateTime(reader.GetOrdinal("TanggalLahirAnak"));

                                // Calculate the age
                                int ageInMonths = CalculateAge(birthDate, selectedDate);

                                // Determine gender
                                string jenisKelaminAnak = reader["JenisKelaminAnak"].ToString();

                                // Visual representation based on age range and gender
                                string imagePath = GetImageForAgeAndGender(ageInMonths, jenisKelaminAnak);

                                // Set ImageSource
                                childImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));

                                MessageTextBlock1.Text = $"{NamaAnak}";
                                MessageTextBlock.Text = $"{txtEventTitle.Text}";
                                MessageTextBlock2.Text = $"{selectedDate.ToString("dd")} bulan {selectedDate.ToString("MM")} tahun {selectedDate.ToString("yyyy")}";
                                MessageTextBlock3.Text = $"{NamaAnak}";
                            }
                            else
                            {
                                MessageTextBlock1.Text = "Data anak tidak ditemukan.";
                                MessageTextBlock2.Text = "";
                                MessageTextBlock3.Text = "";
                                MessageTextBlock.Text = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string GetImageForAgeAndGender(int ageInMonths, string jenisKelaminAnak)
        {
            // Determine image path based on age range and gender
            if (ageInMonths >= 0 && ageInMonths <= 2)
            {
                return jenisKelaminAnak.ToLower() == "laki-laki" ? "/Asset/0bln_M.jpg" : "/Asset/0bln_F.jpg";
            }
            else if (ageInMonths >= 3 && ageInMonths <= 6)
            {
                return jenisKelaminAnak.ToLower() == "laki-laki" ? "/Asset/3bln_M.jpg" : "/Asset/pbln_F.jpg";
            }
            else if (ageInMonths >= 7 && ageInMonths <= 11)
            {
                return jenisKelaminAnak.ToLower() == "laki-laki" ? "/Asset/7bln_M.jpg" : "/Asset/7bln_F.jpg";
            }
            else if (ageInMonths >= 12 && ageInMonths <= 18)
            {
                return jenisKelaminAnak.ToLower() == "laki-laki" ? "/Asset/12bln_M.jpg" : "/Asset/12bln_F.jpg";
            }
            else if (ageInMonths > 18)
            {
                return jenisKelaminAnak.ToLower() == "laki-laki" ? "/Asset/19bln_M.jpg" : "/Asset/19bln_F.jpg";
            }
            else
            {
                // Handle other age ranges as needed
                return string.Empty;
            }
        }
        private int CalculateAge(DateTime birthDate, DateTime currentDate)
        {
            return AgeHelper.CalculateAge(birthDate, currentDate);
        }

        private void UpdateAge()
        {
            int age = AgeHelper.CalculateAge(birthDate, DateTime.Today);
            string ageChild = age.ToString();
            ageInMonths.Text = $"{ageChild} months";  // Update teks umur di XAML
        }

    }
}
