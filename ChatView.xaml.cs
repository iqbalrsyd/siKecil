using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace siKecil
{
    public partial class ChatView : Window
    {
        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();

        public ChatView()
        {
            InitializeComponent();
            chatItemsControl.ItemsSource = chatMessages;
        }

        private void KirimButton_Click(object sender, RoutedEventArgs e)
        {
            User pengirim = new User { UserName = "Pengirim" }; // Gantilah dengan objek pengirim yang sesuai
            User penerima = new User { UserName = "Penerima" }; // Gantilah dengan objek penerima yang sesuai

            SendMessage(pengirim, penerima, pesanTextBox.Text);
        }

        private void SendMessage(User pengirim, User penerima, string message)
        {
            // Simulasikan timestamp
            string timestamp = DateTime.Now.ToString("HH:mm");

            // Menambahkan pesan ke koleksi
            chatMessages.Add(new ChatMessage { SenderName = pengirim.UserName, Message = message, Timestamp = timestamp, MessageAlignment = HorizontalAlignment.Right, MessageBackground = "LightGreen", MessageMargin = new Thickness(0,0,0,20)});;
            chatMessages.Add(new ChatMessage { SenderName = penerima.UserName, Message = message, Timestamp = timestamp, MessageAlignment = HorizontalAlignment.Left, MessageBackground = "LightGray", MessageMargin = new Thickness(20, 0, 0, 0) });

            // Membersihkan textbox setelah mengirim
            pesanTextBox.Text = "";
        }
    }

    public class User
    {
        public string UserName { get; set; }
    }

    public class ChatMessage
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public string Timestamp { get; set; }
        public HorizontalAlignment MessageAlignment { get; set; }
        public string MessageBackground { get; set; }
        public Thickness MessageMargin {  get; set; }
    }
}