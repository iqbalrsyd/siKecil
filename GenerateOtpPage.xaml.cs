using System.Net.Mail;
using System.Net;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace siKecil
{
    public partial class GenerateOtpPage : Page
    {
        public event Action<string> SuccessfulOtpVerification;

        private string randomCodeOTP;
        private string EmailAddress;
    
        public GenerateOtpPage(string EmailAddress)
        {
            InitializeComponent();
            this.EmailAddress = EmailAddress;
            this.Background = new SolidColorBrush(Colors.LightGray);
            GenerateAndSendOtp();
        }

        private void OnOTPTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
            {
                int index = int.Parse(box.Name.Substring(box.Name.Length - 1));

                if (!string.IsNullOrEmpty(box.Text) && index < 6)
                {
                    var nextBox = this.FindName("textBox" + (index + 1)) as TextBox;
                    if (nextBox != null)
                    {
                        nextBox.Focus();
                    }
                }

                if (string.IsNullOrEmpty(box.Text) && Keyboard.IsKeyDown(Key.Back) && index > 1)
                {
                    var previousBox = this.FindName("textBox" + (index - 1)) as TextBox;
                    if (previousBox != null)
                    {
                        previousBox.Text = string.Empty;
                        previousBox.Focus();
                    }
                }
            }
        }
        private void GenerateAndSendOtp()
        {
            try
            {
                string from, pass, messageBody;
                Random rand = new Random();
                randomCodeOTP = (rand.Next(999999)).ToString();
                MailMessage message = new MailMessage();
                string to = (EmailAddress).ToString();

                string domain = to.Split('@')[1].ToLower();

                if (domain.Contains("gmail.com"))
                {
                    from = "rasyad3003@gmail.com";
                    pass = "ymvn frzk ejbw ihwv";
                    messageBody = "Your OTP code is " + randomCodeOTP;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(from, pass);

                    message.To.Add(to);
                    message.From = new MailAddress(from);
                    message.Body = messageBody;
                    message.Subject = "Password OTP Code Application siKecil";

                    smtp.Send(message);

                    MessageBox.Show("Code Sent Successfully");
                }
                else
                {
                    MessageBox.Show("Unsupported email domain");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SubmitOtp_Click(object sender, RoutedEventArgs e)
        {
            string enteredOtp = GetEnteredOtp();
            if (enteredOtp == randomCodeOTP)
            {
                MessageBox.Show("OTP berhasil terverifikasi");

                SuccessfulOtpVerification?.Invoke(EmailAddress);
            }
            else
            {
                MessageBox.Show("OTP salah");
            }
        }

        private string GetEnteredOtp()
        {
            string enteredOtp = string.Empty;
            for (int i = 1; i <= 6; i++)
            {
                TextBox textbox = FindName($"textBox{i}") as TextBox;
                if (textbox != null)
                {
                    enteredOtp += textbox.Text;
                }
            }
            return enteredOtp;
        }
    }
}
