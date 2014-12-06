using System.IO;
using System.Threading.Tasks;

namespace SCI.Adapters.Device
{
    public interface IImageHelper
    {
        Task<object> GetImageSourceAsync(Stream imageStream);
    }
}