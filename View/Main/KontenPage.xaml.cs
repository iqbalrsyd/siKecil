using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using siKecil.Infrastructure;



namespace siKecil.View.Main
{
    /// <summary>
    /// Interaction logic for KontenPage.xaml
    /// </summary>
    public partial class KontenPage : Page
    {
        private int ID_Artikel;
        Connection connectionHelper = new Connection();
        private ArtikelDisplay artikel;
        public KontenPage(int ID_Artikel)
        {
            InitializeComponent();
            Title = "Tips dan Informasi";
            this.ID_Artikel = ID_Artikel;
            artikel = GetArtikelFromDatabase();
            DisplayKonten();
        }

        private ArtikelDisplay GetArtikelFromDatabase()
        {
            ArtikelDisplay konten = new ArtikelDisplay();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                int id_artikel = this.ID_Artikel;
                string getPerson = "SELECT ID_Artikel, Judul, Isi FROM Informasi WHERE ID_Artikel = @ID_Artikel";

                using (SqlCommand command = new SqlCommand(getPerson, sqlCon))
                {
                    command.Parameters.AddWithValue("@ID_Artikel", id_artikel);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            konten.Id_Artikel = reader.GetInt32(0);
                            konten.Judul = reader.GetString(1);
                            konten.Isi = reader.GetString(2);
                        }
                    }
                }
            }
            return konten;
        }

        private void DisplayKonten()
        {
            string judul = artikel.Judul;
            string isi = artikel.Isi;

            JudulArtikel.Text = $"{judul}";
            IsiKonten.Text = $"{isi}";
        }

        private void KontenPicker_SelectedChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DisplayKonten();
        }

        private void NavigateToInformationPage()
        {
            NavigationService?.Navigate(new InformationPage());
        }

        private void InformationPage_Back(object sender, RoutedEventArgs e)
        {
            NavigateToInformationPage();
        }
    }
    public class ArtikelDisplay
    {
        public int Id_Artikel { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
    }
}
