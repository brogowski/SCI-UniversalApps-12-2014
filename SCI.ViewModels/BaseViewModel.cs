using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace SCI.ViewModels
{
    public abstract class BaseViewModel : ViewModelBase
    {
        public virtual bool BackButtonIsVisible { get { return true; } }

        public ICommand LoadedCommand
        {
            get { return new RelayCommand(Loaded); }
        }

        public ICommand BackCommand
        {
            get { return new RelayCommand(Back); }
        }

        protected virtual void Back() { }

        protected virtual void Loaded() { }
    }
}
