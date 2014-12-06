using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using SCI.Adapters.ApplicationState;
using SCI.Adapters.DataAccess;
using SCI.Adapters.UserInfo;
using SCI.BL.Entities;
using SCI.BL.Factories;

namespace SCI.ViewModels
{
    public class TextViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IWallRepository _repo;
        private readonly IWallEntryCreator _creator;
        private readonly IUserNameProvider _userInfo;
        private string _textBoxContent;
        private bool _sendButtonIsEnabled = true;

        public TextViewModel(INavigationService navigationService, IWallRepository repo, IWallEntryCreator creator, IUserNameProvider userInfo)
        {
            _navigationService = navigationService;
            _repo = repo;
            _creator = creator;
            _userInfo = userInfo;
        }

        public string TextBoxContent
        {
            get { return _textBoxContent; }
            set { _textBoxContent = value; RaisePropertyChanged(() => TextBoxContent); }
        }

        public ICommand SendCommand
        {
            get { return new RelayCommand(Send); }
        }

        public bool SendButtonIsEnabled
        {
            get { return _sendButtonIsEnabled; }
            set { _sendButtonIsEnabled = value; RaisePropertyChanged(() => SendButtonIsEnabled); }
        }

        public ICommand SendWallEntry
        {
            get { return new RelayCommand(Send); }
        }

        private async void Send()
        {
            SendButtonIsEnabled = false;
            try
            {
                await SendTextWallEntry();
            }
            catch (Exception)
            {                                
            }
            SendButtonIsEnabled = true;
            TextBoxContent = "";
        }

        private async Task SendTextWallEntry()
        {
            var info = new TextWallEntryInfo
            {
                Author = _userInfo.GetUserName(),
                Content = TextBoxContent,
                Title = TextBoxContent.Substring(0, Math.Min(TextBoxContent.Length, 25)) + "..."
            };
            var wallEntry = _creator.CreateTextWallEntry(info);
            await _repo.SaveAsync(wallEntry);
        }

        protected override void Back()
        {                        
            _navigationService.GoBack();
        }
    }
}
