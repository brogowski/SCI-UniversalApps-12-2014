using System;
using Windows.Graphics.Display;
using SCI.Adapters.Device;

namespace SCI.App.Adapters.Device
{
    public class OrientationChangeHandler : IOrientationChangeHandler
    {
        private DisplayInformation _getForCurrentView;

        public event EventHandler<ScreenOrientation> OrientationChanged;

        public void StartListening()
        {
            _getForCurrentView = DisplayInformation.GetForCurrentView();
            _getForCurrentView.OrientationChanged += ScreenOrientationChanged;
        }

        public ScreenOrientation GetCurrentOrientation()
        {
            return (ScreenOrientation)_getForCurrentView.CurrentOrientation;
        }

        private void ScreenOrientationChanged(DisplayInformation sender, object args)
        {
            if (OrientationChanged != null)
            {
                OrientationChanged(this, (ScreenOrientation)sender.CurrentOrientation);
            }
        }

        public void Dispose()
        {
            _getForCurrentView.OrientationChanged -= ScreenOrientationChanged;            
        }
    }
}