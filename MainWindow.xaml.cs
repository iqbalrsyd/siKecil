using siKecil.Model;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void HomePage_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(User_ID);
            mainFrame.NavigationService.Navigate(homePage);
        }

        private void DiaryAnakPage_Click(object sender, RoutedEventArgs e)
        {
            DiaryAnakPage diaryAnakPage = new DiaryAnakPage(User_ID);
            mainFrame.NavigationService.Navigate(diaryAnakPage);
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

    }
}