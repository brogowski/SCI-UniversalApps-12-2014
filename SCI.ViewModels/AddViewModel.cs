using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SCI.Adapters.ApplicationState;

namespace SCI.ViewModels
{
    public class AddViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public AddViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand GoToCameraCommand
        {
            get { return new RelayCommand(GoToCamera); }
        }

        public ICommand GoToTextCommand
        {
            get { return new RelayCommand(GoToText); }
        }

        private void GoToText()
        {
            _navigationService.Navigate("TextEntry");
        }

        protected override void Back()
        {                        
            _navigationService.GoBack();
        }

        private void GoToCamera()
        {
            _navigationService.Navigate("CameraEntry");
        }
    }
}
