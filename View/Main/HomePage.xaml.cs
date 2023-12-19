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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly string User_ID;
        public HomePage(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;

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
                            greetingText.Text = $"Hallo {firstname}!";
                        }
                    }
                }
            }
        }
    }
}
