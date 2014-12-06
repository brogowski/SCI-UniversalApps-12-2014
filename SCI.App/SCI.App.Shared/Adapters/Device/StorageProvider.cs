using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using SCI.Adapters.Device;

namespace SCI.App.Adapters.Device
{
    public class StorageProvider : IStorageProvider
    {
        private const string _containerName = "Wall";
        private const string _lastSelectedIndexKey = "LastSelectedIndex";

        public StorageProvider()
        {
            InitializeContainer();
        }

        public async Task SaveToPhotoLibraryAsync(Stream imageStream)
        {
            var file = await KnownFolders.CameraRoll.CreateFileAsync("WallImage" + DateTime.Now.ToFileTime() + ".jpg",
                CreationCollisionOption.GenerateUniqueName);

            using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                imageStream.Position = 0;
                var bytes = new byte[imageStream.Length];
                await imageStream.ReadAsync(bytes, 0, bytes.Length);
                using (var dataWriter = new DataWriter(fileStream))
                {
                    dataWriter.WriteBytes(bytes);
                    await dataWriter.StoreAsync();
                }
            }
        }

        public void SaveLastSelectedIndex(int selectedIndex)
        {
            GetRoamingValues()[_lastSelectedIndexKey] = selectedIndex;
        }

        public int GetLastSelectedIndex()
        {            
            return (int) GetRoamingValues()[_lastSelectedIndexKey];
        }

        private void InitializeContainer()
        {
            if (!ApplicationData.Current.RoamingSettings.Containers.ContainsKey(_containerName))
                ApplicationData.Current.RoamingSettings.CreateContainer(_containerName, ApplicationDataCreateDisposition.Always);
            if (!GetRoamingValues().ContainsKey(_lastSelectedIndexKey))
                GetRoamingValues().Add(_lastSelectedIndexKey, 0);            
        }

        private IPropertySet GetRoamingValues()
        {
            return ApplicationData.Current.RoamingSettings.Containers[_containerName].Values;
        }
    }
}
