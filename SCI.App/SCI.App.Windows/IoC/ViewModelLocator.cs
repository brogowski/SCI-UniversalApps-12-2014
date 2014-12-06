using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SCI.Adapters;
using SCI.Adapters.ApplicationState;
using SCI.Adapters.DataAccess;
using SCI.Adapters.Device;
using SCI.App.Adapters;
using SCI.App.Adapters.ApplicationState;
using SCI.App.Adapters.DataAccess;
using SCI.App.Adapters.Device;
using SCI.BL.Factories;
using SCI.ViewModels;
using SCI.Adapters.Resources;
using SCI.App.Adapters.Resources;
using SCI.Adapters.UserInfo;
using SCI.App.Adapters.UserInfo;

namespace SCI.App.IoC
{
    public class ViewModelLocator
    {
        private static INavigationService _navigationService;
        private static IPhotoDevice _photoDevice;
        private static IStateManager _stateManager;

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                // SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WallViewModel>();
            SimpleIoc.Default.Register<AddViewModel>();
            SimpleIoc.Default.Register<CameraViewModel>();
            SimpleIoc.Default.Register<TextViewModel>();

            SimpleIoc.Default.Register<IResourcesLoader, ResourcesLoader>();
            SimpleIoc.Default.Register<IUserNameProvider, UserNameProvider>();
            SimpleIoc.Default.Register<INavigationService>(GetNavigationServiceSingleton);
            SimpleIoc.Default.Register<IWallRepository>(() => new ApiWallRepository("http://localhost:11798/api/Wall"));

            SimpleIoc.Default.Register<IImageHelper, ImageHelper>();
            SimpleIoc.Default.Register<IOrientationChangeHandler, OrientationChangeHandler>();
            SimpleIoc.Default.Register<IPreviewOrientationHelper, PreviewOrientationHelper>();
            SimpleIoc.Default.Register<IPhotoDevice>(GetPhotoDeviceSingleton);

            SimpleIoc.Default.Register<IThreadHelper, ThreadHelper>();
            SimpleIoc.Default.Register<IWallEntryCreator, WallEntryFactory>();
            SimpleIoc.Default.Register<IStorageProvider, StorageProvider>();

            SimpleIoc.Default.Register<IStateManager>(GetStateManagerSingleton);
        }

        private static IStateManager GetStateManagerSingleton()
        {
            return _stateManager ?? (_stateManager = new StateManager());
        }

        private static IPhotoDevice GetPhotoDeviceSingleton()
        {
            return _photoDevice ?? (_photoDevice = new PhotoDevice());
        }

        private static INavigationService GetNavigationServiceSingleton()
        {
            return _navigationService ?? (_navigationService = new NavigationService(Window.Current.Content as Frame));
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public WallViewModel Wall
        {
            get { return ServiceLocator.Current.GetInstance<WallViewModel>(); }
        }

        public CameraViewModel Camera
        {
            get { return ServiceLocator.Current.GetInstance<CameraViewModel>(); }
        }

        public AddViewModel Add
        {
            get { return ServiceLocator.Current.GetInstance<AddViewModel>(); }
        }

        public TextViewModel Text
        {
            get { return ServiceLocator.Current.GetInstance<TextViewModel>(); }
        }
    }
}
