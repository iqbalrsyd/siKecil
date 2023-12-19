using siKecil.Model;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
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

    }

}
