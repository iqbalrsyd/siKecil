using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace siKecil
{
    public partial class UserEnter : Window
    {
        private string User_ID;
        public UserEnter()
        {
            InitializeComponent();
            Loaded += UserEnter_Loaded;
        }

        private void UserEnter_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            Uri pageUri = new Uri("LoginPage.xaml", UriKind.Relative);
            mainFrame.NavigationService.Navigate(pageUri);
        }

        public void NavigateToLoginPage()
        {
            mainFrame.Navigate(new LoginPage());
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            Button signupButton = (Button)sender;
            signupButton.Background = Brushes.DarkBlue;
            signupButton.Foreground = Brushes.LightBlue;

            SignupPage signupPage = new SignupPage(User_ID);
            mainFrame.NavigationService.Navigate(signupPage);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Button loginButton = (Button)sender;
            loginButton.Background = Brushes.DarkBlue;
            loginButton.Foreground = Brushes.LightBlue;

            LoginPage loginPage = new LoginPage();
            mainFrame.NavigationService.Navigate(loginPage);
        }

        private void mainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
    }
}