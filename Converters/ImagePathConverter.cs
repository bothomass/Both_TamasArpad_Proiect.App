using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Both_TamasArpad_Proiect.Converters
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string title)
            {
                // Replace spaces with underscores and append .jpg
                return $"{title.Replace(" ", "_")}.jpg";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
