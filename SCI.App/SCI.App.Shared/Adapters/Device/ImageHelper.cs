using System;
using System.IO;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using SCI.Adapters.Device;

namespace SCI.App.Adapters.Device
{
    public class ImageHelper : IImageHelper
    {
        public object GetImageSource(Stream stream)
        {
            var bitmap = new BitmapImage();
            bitmap.SetSource(stream.AsRandomAccessStream());
            return bitmap;
        }

        public async Task<object> GetImageSourceAsync(Stream stream)
        {
            var bitmap = new BitmapImage();
            await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            return bitmap;
        }
    }
}
