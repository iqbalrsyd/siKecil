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
using siKecil.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;
using siKecil.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace siKecil.View.Main
{
    /// <summary>
    /// Interaction logic for InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Page
    {
        private ObservableCollection<Artikel> Artikels;
        Connection connectionHelper = new Connection();
        public InformationPage()
        {
            InitializeComponent();
            Artikels = LoadArtikel();
            ListBoxArtikel.ItemsSource = Artikels;
        }

        private ObservableCollection<Artikel> LoadArtikel()
        {
            List <Artikel> artikel = new List<Artikel>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                try
                {
                    sqlCon.Open();

                   
                    string query = $"SELECT ID_Artikel, Judul, Isi FROM Informasi";
                    using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Artikel artikel1 = new Artikel
                                {
                                    ID_Artikel = Convert.ToInt32(reader["ID_Artikel"]),
                                    Judul = reader.IsDBNull(reader.GetOrdinal("Judul")) ? string.Empty : reader["Judul"].ToString(),
                                    Isi = reader.IsDBNull(reader.GetOrdinal("Isi")) ? string.Empty : reader["Isi"].ToString()
                                };

                                artikel.Add(artikel1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            return new ObservableCollection<Artikel>(artikel);
        }

        private void ArtikelList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxArtikel.SelectedItem != null)
            {
                Artikel selectedArtikel = (Artikel)ListBoxArtikel.SelectedItem;
                int selectedArtikelID = selectedArtikel.ID_Artikel;

                // Pass the selectedArtikelID to the new page for navigation
                // Replace "YourNewPage" with the actual name of the page you want to navigate to
                NavigationService?.Navigate(new KontenPage(selectedArtikelID));
            }
        }
    }

    public class Artikel
    {
        public int ID_Artikel { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
    }
}
