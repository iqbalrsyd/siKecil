using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace siKecil
{
    public static class WatermarkService
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached(
                "Watermark",
                typeof(string),
                typeof(WatermarkService),
                new PropertyMetadata(OnWatermarkChanged)
            );

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if (e.NewValue != null)
                {
                    textBox.GotFocus += TextBox_GotFocus;
                    textBox.LostFocus += TextBox_LostFocus;

                    UpdateWatermark(textBox);
                }
            }
        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.Clear();
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            UpdateWatermark(textBox);
        }

        private static void UpdateWatermark(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetWatermark(textBox);
            }
        }

        public static readonly DependencyProperty WatermarkOpacityProperty =
            DependencyProperty.RegisterAttached(
                "WatermarkOpacity",
                typeof(double),
                typeof(WatermarkService),
                new PropertyMetadata(0.5) 
            );

        public static readonly DependencyProperty WatermarkForegroundProperty =
            DependencyProperty.RegisterAttached(
                "WatermarkForeground",
                typeof(Brush),
                typeof(WatermarkService),
                new PropertyMetadata(Brushes.Gray) 
            );

        public static readonly DependencyProperty WatermarkFontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "WatermarkFontFamily",
                typeof(FontFamily),
                typeof(WatermarkService),
                new PropertyMetadata(new FontFamily("Inter")) 
            );

        public static readonly DependencyProperty WatermarkFontSizeProperty =
            DependencyProperty.RegisterAttached(
                "WatermarkFontSize",
                typeof(int),
                typeof(WatermarkService),
                new PropertyMetadata(18)
            );

        public static double GetWatermarkFontSize(DependencyObject obj)
        {
            return (int)obj.GetValue(WatermarkFontSizeProperty);
        }

        public static void SetWatermarkFontSize(DependencyObject obj, int value)
        {
            obj.SetValue(WatermarkFontSizeProperty, value);
        }

        public static double GetWatermarkOpacity(DependencyObject obj)
        {
            return (double)obj.GetValue(WatermarkOpacityProperty);
        }

        public static void SetWatermarkOpacity(DependencyObject obj, double value)
        {
            obj.SetValue(WatermarkOpacityProperty, value);
        }

        public static Brush GetWatermarkForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(WatermarkForegroundProperty);
        }

        public static void SetWatermarkForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(WatermarkForegroundProperty, value);
        }

        public static FontFamily GetWatermarkFontFamily(DependencyObject obj)
        {
            return (FontFamily)obj.GetValue(WatermarkFontFamilyProperty);
        }

        public static void SetWatermarkFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(WatermarkFontFamilyProperty, value);
        }
    }
}