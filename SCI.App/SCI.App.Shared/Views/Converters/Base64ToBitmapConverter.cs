using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using SCI.App.Helpers;

namespace SCI.App.Views.Converters
{
    public class Base64ToBitmapConverter : IValueConverter
    {
        private static Dictionary<string, BitmapImage> _images = new Dictionary<string, BitmapImage>();

        public object Convert(object value, Type targetType, object parameter, string language)
        {           
            var base64 = value as string;
            if (string.IsNullOrEmpty(base64))
                return null;
            if(!_images.ContainsKey(base64))
                _images.Add(base64, base64.ConvertToBitmapImage());
            return _images[base64];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}