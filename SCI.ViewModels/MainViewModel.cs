using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SCI.Adapters.ApplicationState;
using SCI.Adapters.UserInfo;

namespace SCI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string _userName;
        private readonly IUserNameProvider _userNameProvider;
        private readonly INavigationService _navigationService;

        public MainViewModel(IUserNameProvider userNameProvider, INavigationService navigationService)
        {
            _userNameProvider = userNameProvider;
            _navigationService = navigationService;
            _userName = "Test";
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ICommand GoToWallCommand
        {
            get { return new RelayCommand(GoToWall); }
        }

        private void GoToWall()
        {
            _navigationService.Navigate("Wall");
        }

        protected override async void Loaded()
        {
            UserName = await _userNameProvider.GetUserNameAsync();
        }
    }


}
