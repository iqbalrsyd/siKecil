using System.Data.SqlClient;
using System.Windows;

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

        private void ToProfileView(object sender, RoutedEventArgs e)
        {
            ProfileView profile = new ProfileView(User_ID);
            profile.Show();
            this.Close();
        }

        private void ToRiwayatMedisView(object sender, RoutedEventArgs e)
        {
            RiwayatMedisView Riwayat= new RiwayatMedisView(User_ID);
            Riwayat.Show();
            this.Close();
        }

        private void ToChatView(object sender, RoutedEventArgs e)
        {
            ChatView chat = new ChatView(User_ID);
            chat.Show();
            this.Close();
        }

        private void ToCatatanTumbuhAnak(object sender, RoutedEventArgs e)
        {
            CatatTumbuhAnak Catatan = new CatatTumbuhAnak(User_ID);
            Catatan.Show();
            this.Close();
        }
    }
}