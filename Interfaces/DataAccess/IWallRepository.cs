using System.Linq;
using System.Threading.Tasks;
using SCI.BL.Entities;

namespace SCI.Adapters.DataAccess
{
    public interface IWallRepository
    {
        Task SaveAsync(WallEntry wallEntry);
        Task<IQueryable<WallEntry>> GetAsync();
        Task UpdateAsync(WallEntry wallEntry);
        Task DeleteAsync(WallEntry wallEntry);
    }
}