using SCI.BL.Entities;

namespace SCI.BL.Factories
{
    public interface IWallEntryFactory
    {
        WallEntry Create(string type);
    }
}