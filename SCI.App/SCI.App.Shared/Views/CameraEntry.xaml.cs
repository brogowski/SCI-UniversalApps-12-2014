using Windows.Media.Capture;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using GalaSoft.MvvmLight.Messaging;

namespace SCI.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CameraEntry : BasePage
    {
        public CameraEntry()        
        {
            this.InitializeComponent();           
            Messenger.Default.Register<object>(this, RefreshPreview);
        }

        private void RefreshPreview(object obj)
        {
            CaptureElement.Source = obj as MediaCapture;
            if(CaptureElement.Source != null)
                CaptureElement.Source.StartPreviewAsync();            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}
