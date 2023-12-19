using siKecil.Model;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace siKecil
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
    }

}
