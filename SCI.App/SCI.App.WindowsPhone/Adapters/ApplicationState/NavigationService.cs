using System;
using Windows.UI.Xaml.Controls;
using SCI.Adapters.ApplicationState;

namespace SCI.App.Adapters.ApplicationState
{
    class NavigationService : INavigationService
    {
        private readonly Frame _applicationFrame;
        public NavigationService(Frame applicationFrame)
        {
            _applicationFrame = applicationFrame;

            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (sender, e) =>
            {
                if (_applicationFrame.CanGoBack)
                {
                    _applicationFrame.GoBack();
                    e.Handled = true;
                }
            };
        }

        public void Navigate(string pageName)
        {            
            _applicationFrame.Navigate(Type.GetType("SCI.App.Views." + pageName));            
        }

        public void GoBack()
        {
            _applicationFrame.GoBack();
        }
    }
}
