using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SCI.Adapters.ApplicationState;
using SCI.Adapters.DataAccess;
using SCI.Adapters.Device;
using SCI.BL.Entities;

namespace SCI.ViewModels
{
    public class WallViewModel : BaseViewModel
    {
        private ObservableCollection<WallEntry> _wallItems;
        private readonly IWallRepository _wallRepository;
        private readonly INavigationService _navigationService;
        private readonly IStorageProvider _storageProvider;
        private int _selectedIndex;

        public WallViewModel(IWallRepository wallRepository, INavigationService navigationService, IStorageProvider storageProvider)
        {
            _wallRepository = wallRepository;
            _navigationService = navigationService;
            _storageProvider = storageProvider;
            _wallItems = new ObservableCollection<WallEntry>();
        }

        public ObservableCollection<WallEntry> WallItems
        {
            get { return _wallItems; }
            set
            {
                _wallItems = value;
                RaisePropertyChanged(() => WallItems);
            }
        }
        public ICommand RefreshCommand
        {
            get { return new RelayCommand(Refresh); }
        }
        public ICommand AddCommand
        {
            get { return new RelayCommand(GoToAdd);}
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; RaisePropertyChanged(() => SelectedIndex); }
        }
        public ICommand SelectionChangedCommand
        {
            get { return new RelayCommand(SelectionChanged); }
        }

        protected override async void Loaded()
        {
            await GetAllEntriesFromRepo();            
        }

        private void ScrollToRecentSelectedItem()
        {
            var index = _storageProvider.GetLastSelectedIndex();
            if(WallItems.Count > index && index != -1)
                Messenger.Default.Send<object>(WallItems.ElementAt(index));
            SelectedIndex = index;
        }
        private void SelectionChanged()
        {
            if(SelectedIndex != -1)
                _storageProvider.SaveLastSelectedIndex(SelectedIndex);
        }
        private void GoToAdd()
        {
            _navigationService.Navigate("AddEntry");
        }
        private async void Refresh()
        {
            await GetAllEntriesFromRepo();
        }
        private async Task GetAllEntriesFromRepo()
        {
            var result = await _wallRepository.GetAsync();
            _wallItems.Clear();
            InsertItems(result);
            ScrollToRecentSelectedItem();
        }
        private void InsertItems(IEnumerable<WallEntry> result)
        {
            foreach (var wallEntry in result)
            {
                WallItems.Add(wallEntry);
            }
            RaisePropertyChanged(() => WallItems);
        }
    }
}
