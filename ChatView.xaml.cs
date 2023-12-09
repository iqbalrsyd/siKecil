using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using System.Linq;

namespace siKecil
{
    public partial class ChatView : Window
    {
        private ObservableCollection<User> KontakList;
        private ObservableCollection<ChatMessage> chatMessages;
        private string User_ID;
        Connection connectionHelper = new Connection();
        private int yourUserID;
        public ChatView(string User_ID)
        {
            InitializeComponent();
            this.User_ID = User_ID;
            KontakList = LoadKontakData(User_ID);
            chatMessages = new ObservableCollection<ChatMessage>();
            chatItemsControl.ItemsSource = chatMessages;
            kontakItemsControl.DataContext = KontakList;
            SetYourUserId();
        }

        private ObservableCollection<User> LoadKontakData(string User_ID)
        {
            List<User> users = new List<User>();

            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();
                string user_id = this.User_ID;
                string query = $"SELECT User_ID, Username FROM Users WHERE User_ID != '{user_id}'";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                UserIdKontak = reader.GetInt32(reader.GetOrdinal("user_id")),
                                UserNameKontak = reader.GetString(reader.GetOrdinal("Username"))
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

                string queryMessage = "SELECT * FROM Chat WHERE (Penerima_ID = @UserId OR Pengirim_ID = @UserId) AND (Penerima_ID = @Penerima_ID OR Pengirim_ID = @Penerima_ID) ORDER BY Timestamp";
                using (SqlCommand cmd = new SqlCommand(queryMessage, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@UserId", User_ID);
                    cmd.Parameters.AddWithValue("@Penerima_ID", selectedUser.UserIdKontak);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Message message = new Message
                            {
                                SenderId = reader.GetInt32(reader.GetOrdinal("Pengirim_ID")),
                                ReceiverId = reader.GetInt32(reader.GetOrdinal("Penerima_ID")),
                                Timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp")),
                                Content = reader.GetString(reader.GetOrdinal("isiChat"))
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
            chatItemsControl.ItemsSource = null;
            chatItemsControl.ItemsSource = chatMessages;
        }

        private void DisplayMessages(List<Message> messages)
        {
            chatMessages.Clear();
            
            foreach (Message message in messages)
            {
                User pengirim = LoadUserById(message.SenderId);
                User penerima = LoadUserById(message.ReceiverId);

                HorizontalAlignment alignment;

                if (yourUserID == message.ReceiverId)
                {
                    alignment = HorizontalAlignment.Left;
                }
                else if (yourUserID == message.SenderId)
                {
                    alignment = HorizontalAlignment.Right;
                }
                else
                {
                    alignment = HorizontalAlignment.Center;
                }

                ChatMessage chatMessage = new ChatMessage
                {
                    Message = message.Content,
                    Timestamp = message.Timestamp.ToString("HH:mm"),
                    MessageAlignment = alignment
                };

                if (alignment == HorizontalAlignment.Right)
                {
                    chatMessage.SenderName = pengirim.UserNameKontak;
                    chatMessage.MessageBackground = "LightGreen";
                    chatMessage.MessageMargin = new Thickness(0, 0, 0, 20);
                }
                else if (alignment == HorizontalAlignment.Left)
                {
                    chatMessage.SenderName = penerima.UserNameKontak;
                    chatMessage.MessageBackground = "LightGray";
                    chatMessage.MessageMargin = new Thickness(20, 0, 0, 0);
                }
                else
                {
                    chatMessage.SenderName = "DefaultUserName";
                    chatMessage.MessageBackground = "DefaultBackground";
                    chatMessage.MessageMargin = new Thickness(0);
                }

                chatMessages.Add(chatMessage);
            }
            pesanTextBox.Text = "";
        }

        private void SetYourUserId()
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
                                yourUserID = reader.GetInt32(reader.GetOrdinal("User_ID"));
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

        private User LoadUserById(int userId)
        {
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string query = "SELECT User_ID, Username FROM Users WHERE User_ID = @User_ID";
                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserIdKontak = reader.GetInt32(reader.GetOrdinal("User_ID")),
                                UserNameKontak = reader.GetString(reader.GetOrdinal("Username"))
                            };
                        }
                    }
                }
            }

            return null;
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

                string query = "INSERT INTO Chat (User_ID, Pengirim_ID, Penerima_ID, isiChat, Timestamp) VALUES (@User_ID, @Pengirim_ID, @Penerima_ID, @isiChat, @Timestamp)";

                using (SqlCommand cmd = new SqlCommand(query, sqlCon))
                {
                    cmd.Parameters.AddWithValue("@User_ID", User_ID);
                    cmd.Parameters.AddWithValue("@Pengirim_ID", User_ID);
                    cmd.Parameters.AddWithValue("@Penerima_ID", receiver.UserIdKontak);
                    cmd.Parameters.AddWithValue("@isiChat", messageText);
                    cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }

            UpdateChatView(receiver);
        }

        private void KontakItemsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = (User)kontakItemsControl.SelectedItem;

            UpdateChatView(selectedUser);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

         public class ChatManager
         {
             private ChatDbContext dbContext;
             private int yourUserId;

             public ChatManager()
             {
                 dbContext = new ChatDbContext();
             }

             public ObservableCollection<User> LoadKontakData()
             {
                 return new ObservableCollection<User>(dbContext.Users.ToList());
             }

             public List<Message> LoadMessages(User selectedUser)
             {
                 return dbContext.Messages
                     .Where(m => (m.SenderId == yourUserId || m.ReceiverId == yourUserId) &&
                                 (m.SenderId == selectedUser.UserIdKontak || m.ReceiverId == selectedUser.UserIdKontak))
                     .OrderBy(m => m.Timestamp)
                     .ToList();
             }
         }
    }
}

public class User
{
    public int UserIdKontak { get; set; }
    public string UserNameKontak { get; set; }
}

public class Message
{
    public int MessageId { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ChatMessage
{
    public string SenderName { get; set; }
    public string Message { get; set; }
    public string Timestamp { get; set; }
    public HorizontalAlignment MessageAlignment { get; set; }
    public string MessageBackground { get; set; }
    public Thickness MessageMargin { get; set; }
}

public class ChatDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
}
