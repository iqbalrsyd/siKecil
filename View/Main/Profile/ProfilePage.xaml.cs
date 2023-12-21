using System;
using System.Windows;
using System.Windows.Controls;

namespace siKecil.View.Main.Profile
{
    public partial class ProfilePage : Page
    {
        private string User_ID;
        public string PageTitle { get; set; } = "Profile Page";

        public ProfilePage(string User_ID)
        {
            InitializeComponent();
            Title = "Profil";
            this.User_ID = User_ID;
            profileFrame.NavigationService.Navigate(new PengaturanAkunPage(User_ID));
        }

        private void PengaturanAkun_Click(object sender, RoutedEventArgs e)
        {
            PengaturanAkunPage pengaturanAkun = new PengaturanAkunPage(User_ID);
            profileFrame.NavigationService.Navigate(pengaturanAkun);
        }

        private void ProfilOrangTua_Click(object sender, RoutedEventArgs e)
        {
            ProfileOrangTuaPage profileOrangTua = new ProfileOrangTuaPage(User_ID);
            profileFrame.NavigationService.Navigate(profileOrangTua);
        }

        private void ProfilAnak_Click(object sender, RoutedEventArgs e)
        {
            ProfileAnakPage profileAnak = new ProfileAnakPage(User_ID);
            profileFrame.NavigationService.Navigate(profileAnak);
        }
    }
}
