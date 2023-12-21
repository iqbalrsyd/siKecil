using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Media.Imaging;
using siKecil.View.Main.Profile;

namespace siKecil.Infrastructure
{
    public class ImageDisplay
    {
        private BitmapImage GetImageFromDatabase(string user_ID)
        {
            try
            {
                Connection connectionHelper = new Connection();
                using (SqlConnection sqlCon = connectionHelper.GetConn())
                {
                    sqlCon.Open();

                    string selectQuery = "SELECT UserImage FROM Users WHERE User_ID = @User_ID";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, sqlCon))
                    {
                        cmd.Parameters.AddWithValue("@User_ID", user_ID);
                        byte[] imageData = (byte[])cmd.ExecuteScalar();
                        return ByteArrayToImage(imageData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting image from database: {ex.Message}");
                return null;
            }
        }

        public void SaveImageToDatabase(string user_ID, byte[] imageData)
        {
            Connection connectionHelper = new Connection();
            using (SqlConnection sqlCon = connectionHelper.GetConn())
            {
                sqlCon.Open();

                string InsertQuery = "UPDATE Users SET UserImage = @UserImage WHERE User_ID = @User_ID";
                using (SqlCommand cmd4 = new SqlCommand(InsertQuery, sqlCon))
                {
                    cmd4.Parameters.AddWithValue("@UserImage", imageData);
                    cmd4.Parameters.AddWithValue("@User_ID", user_ID);
                    cmd4.ExecuteNonQuery();
                }
            }
        }

        public BitmapImage DisplayImage(string user_ID)
        {
            return GetImageFromDatabase(user_ID);
        }

        public byte[] EditImage(string user_ID, string filePath)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(filePath));

            byte[] imageData = ImageToByteArray(bitmap);

            SaveImageToDatabase(user_ID, imageData);

            return imageData;
        }

        private BitmapImage ByteArrayToImage(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();
            }
            return bitmap;
        }

        public byte[] ImageToByteArray(BitmapImage bitmap)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
