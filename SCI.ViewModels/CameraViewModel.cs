using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SCI.Adapters.ApplicationState;
using SCI.Adapters.DataAccess;
using SCI.Adapters.Device;
using SCI.Adapters.UserInfo;
using SCI.BL.Factories;

namespace SCI.ViewModels
{
    public class CameraViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IPhotoDevice _photoDevice;
        private readonly IImageHelper _imageHelper;
        private readonly IOrientationChangeHandler _orientationHandler;
        private readonly IPreviewOrientationHelper _previewOrientationHelper;
        private readonly IUserNameProvider _userData;
        private readonly IWallEntryCreator _creator;
        private readonly IWallRepository _repo;
        private readonly IStorageProvider _storageProvider;

        private object _imageSource;
        private bool _cameraButtonIsEnabled = true;
        private bool _sendButtonIsEnabled;
        private Stream _imageStream;
        private IStateManager _stateManager;

        public CameraViewModel(INavigationService navigationService, IPhotoDevice photoDevice,
            IImageHelper imageHelper, IOrientationChangeHandler orientationHandler,
            IPreviewOrientationHelper previewOrientationHelper, IUserNameProvider userData,
            IWallEntryCreator creator, IWallRepository repo, IStorageProvider storageProvider, IStateManager stateManager)
        {
            _navigationService = navigationService;
            _photoDevice = photoDevice;
            _imageHelper = imageHelper;
            _orientationHandler = orientationHandler;
            _previewOrientationHelper = previewOrientationHelper;
            _userData = userData;
            _creator = creator;
            _repo = repo;
            _storageProvider = storageProvider;
            _stateManager = stateManager;
            _stateManager.Suspending += Suspending;
            _stateManager.Resuming += Resuming;
        }

        async void Resuming(object sender, System.EventArgs e)
        {
            await _photoDevice.InitializeAsync();
            Messenger.Default.Send(_photoDevice.GetPreviewSource());
        }

        void Suspending(object sender, System.EventArgs e)
        {
            ImageSource = null;
            _photoDevice.Dispose();
        }

        public ICommand CaptureCommand
        {
            get { return new RelayCommand(CaptureImage); }
        }

        public object ImageSource
        {
            get { return _imageSource; }
            set { _imageSource = value; RaisePropertyChanged(() => ImageSource); }
        }

        public bool CameraButtonIsEnabled
        {
            get { return _cameraButtonIsEnabled; }
            set { _cameraButtonIsEnabled = value; RaisePropertyChanged(() => CameraButtonIsEnabled); }
        }

        public bool SendButtonIsEnabled
        {
            get { return _sendButtonIsEnabled; }
            set { _sendButtonIsEnabled = value; RaisePropertyChanged(() => SendButtonIsEnabled); }
        }

        public ICommand SendCommand
        {
            get { return new RelayCommand(Send); }
        }

        private async void Send()
        {
            SendButtonIsEnabled = false;
            await _storageProvider.SaveToPhotoLibraryAsync(_imageStream);
            var entryInfo = new ImageWallEntryInfo
            {
                Author = _userData.GetUserName(),
                Title = "Image",
                Content = _imageStream
            };
            var entry = _creator.CreateImageWallEntry(entryInfo);
            await _repo.SaveAsync(entry);
            SendButtonIsEnabled = true;
        }

        protected override void Back()
        {
            _orientationHandler.OrientationChanged -= OrientationChanged;
            _orientationHandler.Dispose();
            _photoDevice.Dispose();
            _navigationService.GoBack();
        }

        protected override async void Loaded()
        {
            await SetupPreview();
            _orientationHandler.OrientationChanged += OrientationChanged;
            _orientationHandler.StartListening();
            SetupRotation();
        }

        private async Task SetupPreview()
        {
            await _photoDevice.InitializeAsync();
            Messenger.Default.Send(_photoDevice.GetPreviewSource());
        }

        private void SetupRotation()
        {
            _photoDevice.SetPreviewOrientation(
                _previewOrientationHelper.GetPreviewOrientation(
                    _orientationHandler.GetCurrentOrientation()));
        }

        void OrientationChanged(object sender, ScreenOrientation e)
        {
            var orientation = _previewOrientationHelper.GetPreviewOrientation(e);
            _photoDevice.SetPreviewOrientation(orientation);
        }

        private async void CaptureImage()
        {
            CameraButtonIsEnabled = false;
            _imageStream = await _photoDevice.MakePhotoAsync();
            ImageSource = await _imageHelper.GetImageSourceAsync(_imageStream);            
            CameraButtonIsEnabled = true;
            SendButtonIsEnabled = true;
        }
    }
}
