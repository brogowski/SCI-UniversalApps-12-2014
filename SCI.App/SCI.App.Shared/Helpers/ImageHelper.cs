using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace SCI.App.Helpers
{
    public static class ImageHelperExtensions
    {
        public static BitmapImage ConvertToBitmapImage(this string base64)
        {
            var ims = new InMemoryRandomAccessStream();
            var img = new BitmapImage();
            var bytes = Convert.FromBase64String(base64);
            var dataWriter = new DataWriter(ims);
            dataWriter.WriteBytes(bytes);
            dataWriter.StoreAsync().AsTask().Wait();
            ims.Seek(0);
            img.SetSource(ims);
            return img;
        }
    }
}
