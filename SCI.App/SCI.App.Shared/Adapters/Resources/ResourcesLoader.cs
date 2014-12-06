using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using SCI.Adapters.Resources;

namespace SCI.App.Adapters.Resources
{
    class ResourcesLoader : IResourcesLoader
    {
        private ResourceLoader _loader;

        public ResourcesLoader()
        {
            _loader = new ResourceLoader();
        }

        public string GetString(string name)
        {
            return _loader.GetString(name);
        }
    }
}
