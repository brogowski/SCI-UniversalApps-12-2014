using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using SCI.Adapters.ApplicationState;

namespace SCI.App.Adapters.ApplicationState
{
    public class StateManager : IStateManager
    {
        public StateManager()
        {
            Application.Current.Resuming += OnResuming;
            Application.Current.Suspending += OnSuspending;
        }

        void OnResuming(object sender, object e)
        {
            if (Resuming != null)
                Resuming(this, EventArgs.Empty);
        }
        void OnSuspending(object sender, object e)
        {
            if (Suspending != null)
                Suspending(this, EventArgs.Empty);
        }

        public event EventHandler Suspending;
        public event EventHandler Resuming;

        public void Dispose()
        {
            Application.Current.Resuming -= OnResuming;
            Application.Current.Suspending -= OnSuspending;
        }
    }
}
