namespace SCI.Adapters.ApplicationState
{
    public interface INavigationService
    {
        void Navigate(string pageName);
        void GoBack();
    }
}