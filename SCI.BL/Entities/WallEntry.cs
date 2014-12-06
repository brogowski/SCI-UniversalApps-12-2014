using System;

namespace SCI.BL.Entities
{
    public abstract class WallEntry
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public abstract string Type { get; }
    }
}
