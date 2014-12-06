using System;
using System.Threading.Tasks;

namespace SCI.Adapters
{
    public interface IThreadHelper
    {
        Task ExecuteOnMainThread(Func<Task> func);
    }
}