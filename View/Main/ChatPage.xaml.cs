using siKecil.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using siKecil.Model;
using System.Diagnostics;

namespace siKecil.View.Main
{
    public partial class ChatPage : Page
    {
        private ObservableCollection<User> KontakList;
        private ObservableCollection<ChatMessage> chatMessages;
        private string User_ID;
        Connection connectionHelper = new Connection();
        private string yourUserID;
        private ImageDisplay imageDisplay;

        public ChatPage(string User_ID)
        {
            InitializeComponent();
            Title = "Chat";
            this.User_ID = User_ID;
            LoadUserFullName(User_ID);
            SetYourUserId(User_ID);
            KontakList = LoadKontakData(User_ID);
            chatMessages = new ObservableCollection<ChatMessage>();
            chatItemsControl.ItemsSource = chatMessages;
            kontakItemsControl.DataContext = KontakList;

            string defaultImagePath = "pack://application:,,,/Asset/user.png";
            imageDisplay = new ImageDisplay(defaultImagePath);
            UserImage.Source = imageDisplay.DisplayImage(User_ID);
            LoadUserFullName(User_ID);
        }

        private void LoadUserFullName(string user_ID)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string query = $"SELECT FirstName, LastName FROM Users WHERE User_ID = {user_ID}";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", user_ID);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? string.Empty : reader["FirstName"].ToString();
                                string lastName = reader.IsDBNull(reader.GetOrdinal("LastName")) ? string.Empty : reader["LastName"].ToString();

                                string fullName = $"{firstName} {lastName}";

                                UserName.Text = fullName;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error getting user full name: {ex.Message}");
                    }
                }
            }
        }

        private void SetYourUserId(string User_ID)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();
                string query = "SELECT User_ID FROM Users WHERE User_ID = @User_ID";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                yourUserID = reader.IsDBNull(reader.GetOrdinal("User_ID")) ? string.Empty : reader["User_ID"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error getting your user id: {ex.Message}");
                    }
                }
            }
        }
        private ObservableCollection<User> LoadKontakData(string User_ID)
        {
            List<User> users = new List<User>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();
                string query = $"SELECT User_ID, Username FROM Users WHERE User_ID != @User_ID";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserIdKontak = reader.IsDBNull(reader.GetOrdinal("User_ID")) ? string.Empty : reader["User_ID"].ToString(),
                                UserNameKontak = reader.IsDBNull(reader.GetOrdinal("Username")) ? string.Empty : reader["Username"].ToString()
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return new ObservableCollection<User>(users);
        }

        private List<Message> LoadMessages(User selectedUser)
        {
            List<Message> messages = new List<Message>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string queryMessage = "SELECT * FROM Chat WHERE (Penerima_ID = @User_ID OR Pengirim_ID = @User_ID) AND (Penerima_ID = @Penerima_ID OR Pengirim_ID = @Penerima_ID) ORDER BY Timestamp";
                using (SqlCommand cmd = new SqlCommand(queryMessage, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                    cmd.Parameters.AddWithValue("@Penerima_ID", selectedUser.UserIdKontak);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Message message = new Message
                            {
                                SenderId = reader.IsDBNull(reader.GetOrdinal("Pengirim_ID")) ? string.Empty : reader["Pengirim_ID"].ToString(),
                                ReceiverId = reader.IsDBNull(reader.GetOrdinal("Penerima_ID")) ? string.Empty : reader["Penerima_ID"].ToString(),
                                Timestamp = reader.IsDBNull(reader.GetOrdinal("Timestamp")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Timestamp")),
                                Content = reader.IsDBNull(reader.GetOrdinal("IsiChat")) ? string.Empty : reader["IsiChat"].ToString()

                            };

                            messages.Add(message);
                        }
                    }
                }
            }

            return messages;
        }

        private void KontakListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KontakStackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            User selectedUser = (User)stackPanel.DataContext;

            kontakItemsControl.SelectedItem = selectedUser;
        }

        private void UpdateChatView(User selectedUser)
        {
            List<Message> messages = LoadMessages(selectedUser);
            DisplayMessages(messages);
        }

        private User LoadUserById(string userId)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string query = $"SELECT User_ID, Username FROM Users WHERE User_ID = {userId}";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {

                                UserIdKontak = reader.IsDBNull(reader.GetOrdinal("User_ID")) ? string.Empty : reader["User_ID"].ToString(),
                                UserNameKontak = reader.IsDBNull(reader.GetOrdinal("Username")) ? string.Empty : reader["Username"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        private void DisplayMessages(List<Message> messages)
        {
            chatMessages.Clear();

            foreach (Message message in messages)
            {
                User pengirim = LoadUserById(message.SenderId);

                HorizontalAlignment alignment = (yourUserID == message.SenderId) ? HorizontalAlignment.Right : HorizontalAlignment.Left;

                chatMessages.Add(new ChatMessage
                {
                    Message = message.Content,
                    Timestamp = message.Timestamp.ToString("HH:mm"),
                    MessageAlignment = alignment,
                    SenderName = pengirim.UserNameKontak,
                    MessageBackground = (alignment == HorizontalAlignment.Right) ? "#FFC436" : "#FFFFFF",
                });
            }
            ScrollChat.ScrollToEnd();
            pesanTextBox.Text = "";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            User selectedUser = (User)kontakItemsControl.SelectedItem;
            if (selectedUser != null)
            {
                SendMessage(selectedUser, pesanTextBox.Text);
            }
        }

        private void SendMessage(User receiver, string messageText)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string query = "INSERT INTO Chat (User_ID, Pengirim_ID, Penerima_ID, IsiChat, Timestamp) VALUES (@User_ID, @Pengirim_ID, @Penerima_ID, @IsiChat, @Timestamp)";

                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                    cmd.Parameters.AddWithValue("@Pengirim_ID", User_ID);
                    cmd.Parameters.AddWithValue("@Penerima_ID", receiver.UserIdKontak);
                    cmd.Parameters.AddWithValue("@IsiChat", messageText);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }

            UpdateChatView(receiver);
        }

        private void KontakItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = (User)kontakItemsControl.SelectedItem;
            if (selectedUser != null)
            {
                string defaultImagePath = "pack://application:,,,/Asset/user.png";
                imageDisplay = new ImageDisplay(defaultImagePath);
                ReceiverImage.Source = imageDisplay.DisplayImage(selectedUser.UserIdKontak);

                LoadUserFullName(selectedUser.UserIdKontak);
            }

            UpdateChatView(selectedUser);
        }
    }
}