using System;
using System.IO;
using System.Threading.Tasks;
using SCI.App.Views;

namespace SCI.Adapters.Device
{
    public interface IPhotoDevice : IDisposable
    {
        Task InitializeAsync();
        object GetPreviewSource();
        Task<Stream> MakePhotoAsync();
        void SetPreviewOrientation(PreviewOrientation orientation);
    }
}