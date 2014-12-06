using System.Threading.Tasks;

namespace SCI.Adapters.UserInfo
{
    public interface IUserNameProvider
    {
        string GetUserName();
        Task<string> GetUserNameAsync();
    }
}