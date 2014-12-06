using System;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using SCI.Adapters.Device;
using System.IO;
using System.Threading.Tasks;
using SCI.App.Views;

namespace SCI.App.Adapters.Device
{
    public class PhotoDevice : IPhotoDevice
    {
        private MediaCapture _mediaCapture;
        private LowLagPhotoCapture _capture;

        public async Task InitializeAsync()
        {
            _mediaCapture = new MediaCapture();
            await _mediaCapture.InitializeAsync();            
            _mediaCapture.VideoDeviceController.PrimaryUse = CaptureUse.Photo;
            _capture = await _mediaCapture.PrepareLowLagPhotoCaptureAsync(GetImageEncodingProperties());                        
        }

        public object GetPreviewSource()
        {
            return _mediaCapture;
        }

        public void SetPreviewOrientation(PreviewOrientation orientation)
        {
            _mediaCapture.SetPreviewRotation(ConvertOrientationToRotation(orientation));
        }

        private VideoRotation ConvertOrientationToRotation(PreviewOrientation orientation)
        {
            return (VideoRotation)Enum.Parse(typeof(VideoRotation), orientation.ToString());
        }

        private ImageEncodingProperties GetImageEncodingProperties()
        {
            var imageEncodingProperties = ImageEncodingProperties.CreateJpeg();
            imageEncodingProperties.Height = 480;
            imageEncodingProperties.Width = 640;
            return imageEncodingProperties;
        }

        public async Task<Stream> MakePhotoAsync()
        {            
            var photo = await _capture.CaptureAsync();
            return photo.Frame.AsStream();
        }        

        public void Dispose()
        {
            _capture.FinishAsync().GetResults();
            _mediaCapture.StopPreviewAsync();
            _mediaCapture.Dispose();
        }
    }
}