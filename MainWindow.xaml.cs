using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace siKecil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string User_ID;

        public MainWindow(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            Loaded += MainWindow_Loaded;

            Connection connectionHelper = new Connection();

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
                            greetingText.Text=$"Hallo {firstname}!";
                        }
                    }
                }
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void NavigateToView(Type viewType)
        {
            // Membuat instance kelas dan menavigasi ke halaman tersebut
            var viewInstance = Activator.CreateInstance(viewType, User_ID);
            MainFrame.Navigate(viewInstance);
        }


        private void ProfileIcon_Click(object sender, RoutedEventArgs e)
        {
            NavigateToView(typeof(ProfileView));
        }

        private void NotesIcon_Click(object sender, RoutedEventArgs e)
        {
            NavigateToView(typeof(CatatTumbuhAnak));
        }

        private void ChatIcon_Click(object sender, RoutedEventArgs e)
        {
            NavigateToView(typeof(ChatView));
        }

        private void InfoIcon_Click(object sender, RoutedEventArgs e)
        {
            NavigateToView(typeof(RiwayatMedisView));
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            // Mendapatkan sender sebagai elemen Image yang diklik
            Image clickedImage = sender as Image;

            // Debug: Cek apakah clickedImage null atau tidak
            if (clickedImage == null)
            {
                Debug.WriteLine("clickedImage is null.");
                return;
            }

            // Debug: Cek Tag dari clickedImage
            Debug.WriteLine($"Image Tag: {clickedImage.Tag}");

            // Mendapatkan nama kelas dari Tag
            string className = clickedImage.Tag as string;

            // Debug: Cek apakah className diambil dengan benar
            Debug.WriteLine($"Class Name: {className}");

            // Panggil metode sesuai dengan kelas
            switch (className)
            {
                case "User":
                    NavigateToView(typeof(ProfileView));
                    break;
                case "Info":
                    NavigateToView(typeof(RiwayatMedisView));
                    break;
                case "Chat":
                    NavigateToView(typeof(ChatView));
                    break;
                case "Notes":
                    NavigateToView(typeof(CatatTumbuhAnak));
                    break;
                // Tambahkan case lain jika diperlukan
                default:
                    Debug.WriteLine($"Unhandled class: {className}");
                    break;
            }
        }

    }

}
