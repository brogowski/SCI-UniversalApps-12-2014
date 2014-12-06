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
