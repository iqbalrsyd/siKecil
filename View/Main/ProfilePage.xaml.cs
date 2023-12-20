using System;
using System.Collections.Generic;
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
using siKecil.Model;
using siKecil.Infrastructure;

namespace siKecil.View.Main
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private string User_ID;
        public ProfilePage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
        }

        private void PengaturanAkun_Click(object sender, RoutedEventArgs e)
        { }

        private void ProfilOrangTua_Click(object sender, RoutedEventArgs e)
        { }

        private void ProfilAnak_Click(object sender, RoutedEventArgs e)
        { }
    }
}
