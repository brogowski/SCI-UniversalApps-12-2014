using System;

namespace SCI.Adapters.ApplicationState
{
    public interface IStateManager : IDisposable
    {
        event EventHandler Suspending;
        event EventHandler Resuming;
    }
}