using System;
using System.IO;
using SCI.BL.Entities;

namespace SCI.BL.Factories
{
    public class WallEntryFactory : IWallEntryFactory, IWallEntryCreator
    {
        public WallEntry Create(string type)
        {
            switch (type)
            {
                case "text":
                    return new TextWallEntry();
                case "image":
                    return new ImageWallEntry();
            }
            return null;
        }

        public TextWallEntry CreateTextWallEntry(TextWallEntryInfo entryInfo)
        {
            return new TextWallEntry
            {
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Author = entryInfo.Author,
                Content = entryInfo.Content,
                Title = entryInfo.Title
            };
        }

        public ImageWallEntry CreateImageWallEntry(ImageWallEntryInfo entryInfo)
        {
            return new ImageWallEntry
            {
                Id = Guid.NewGuid(),
                TimeStamp = DateTime.Now,
                Author = entryInfo.Author,
                Base64Content = ConvertToBase64(entryInfo.Content),
                Title = entryInfo.Title
            };
        }

        private string ConvertToBase64(Stream content)
        {
            var data = new byte[content.Length];
            content.Position = 0;
            content.Read(data, 0, (int) content.Length);
            return Convert.ToBase64String(data);
        }
    }
}
