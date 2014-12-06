using System.IO;
using System.Threading.Tasks;

namespace SCI.Adapters.Device
{
    public interface IStorageProvider
    {
        Task SaveToPhotoLibraryAsync(Stream imageStream);
        void SaveLastSelectedIndex(int selectedIndex);
        int GetLastSelectedIndex();
    }
}