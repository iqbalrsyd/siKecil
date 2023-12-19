using siKecil.Model;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using siKecil.Infrastructure;

namespace siKecil.View.Main
{
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

        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {
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
        
        private void ProfileView_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView(User_ID);
            profileView.Show();
            this.Close();
        }
    }
}
