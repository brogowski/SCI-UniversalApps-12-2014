using System;
using Windows.Graphics.Display;
using Windows.Media.Capture;
using SCI.Adapters.Device;
using SCI.App.Views;

namespace SCI.App.Adapters.Device
{
    public class PreviewOrientationHelper : IPreviewOrientationHelper
    {
        public PreviewOrientation GetPreviewOrientation(ScreenOrientation screenOrientation)
        {
            return ConvertToPreviewOrientation(GetPreviewOrientation((DisplayOrientations) screenOrientation));
        }

        private VideoRotation GetPreviewOrientation(DisplayOrientations orientation)
        {
            switch (orientation)
            {
                case DisplayOrientations.Landscape:
                    return VideoRotation.None;
                    break;
                case DisplayOrientations.Portrait:
#if WINDOWS_PHONE_APP
                    return VideoRotation.Clockwise270Degrees;
#endif
#if WINDOWS_APP
                    return VideoRotation.None;
#endif
                    break;
                case DisplayOrientations.LandscapeFlipped:
#if WINDOWS_PHONE_APP
                    return VideoRotation.Clockwise180Degrees;
#endif
#if WINDOWS_APP
                    return VideoRotation.None;
#endif
                    break;
                case DisplayOrientations.PortraitFlipped:
#if WINDOWS_PHONE_APP
                    return VideoRotation.Clockwise270Degrees;
#endif
#if WINDOWS_APP
                    return VideoRotation.None;
#endif
                    break;
            }
            return VideoRotation.None;
        }

        private PreviewOrientation ConvertToPreviewOrientation(VideoRotation rotation)
        {
            return (PreviewOrientation) Enum.Parse(typeof (PreviewOrientation), rotation.ToString());
        }
    }
}