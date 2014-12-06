using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCI.Adapters.UserInfo;
using Windows.System.UserProfile;

namespace SCI.App.Adapters.UserInfo
{
    class UserNameProvider : IUserNameProvider
    {
        private string _userName = null;
        public string GetUserName()
        {
            return _userName;
        }

        public async Task<string> GetUserNameAsync()
        {
            if (_userName == null)
            {
                _userName = await UserInformation.GetDisplayNameAsync();
            }
            return _userName;
        }
    }
}
