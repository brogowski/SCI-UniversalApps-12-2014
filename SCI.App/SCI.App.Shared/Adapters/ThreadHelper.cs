using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using SCI.Adapters;

namespace SCI.App.Adapters
{
    class ThreadHelper : IThreadHelper
    {
        public async Task ExecuteOnMainThread(Func<Task> func)
        {
            await Windows.ApplicationModel.Core.CoreApplication.GetCurrentView()
                .CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () => await func.Invoke());            
        }
    }
}
