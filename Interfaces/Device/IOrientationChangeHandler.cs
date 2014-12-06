using System;
using SCI.App.Views;

namespace SCI.Adapters.Device
{
    public interface IOrientationChangeHandler : IDisposable
    {
        event EventHandler<ScreenOrientation> OrientationChanged;
        void StartListening();
        ScreenOrientation GetCurrentOrientation();
    }
}