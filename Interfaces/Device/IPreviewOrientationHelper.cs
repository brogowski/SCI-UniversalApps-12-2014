using SCI.App.Views;

namespace SCI.Adapters.Device
{
    public interface IPreviewOrientationHelper
    {
        PreviewOrientation GetPreviewOrientation(ScreenOrientation screenOrientation);
    }
}