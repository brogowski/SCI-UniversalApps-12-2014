using SCI.BL.Entities;

namespace SCI.BL.Factories
{
    public interface IWallEntryCreator
    {
        TextWallEntry CreateTextWallEntry(TextWallEntryInfo entryInfo);
        ImageWallEntry CreateImageWallEntry(ImageWallEntryInfo entryInfo);
    }
}