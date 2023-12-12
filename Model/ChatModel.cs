using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace siKecil.Model
{

    public class User
    {
        public string UserIdKontak { get; set; }
        public string UserNameKontak { get; set; }
    }

    public class Message
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
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
        public string DateSeparat { get; set; }
        public bool isDateSeparator { get; set; }
    }

}
