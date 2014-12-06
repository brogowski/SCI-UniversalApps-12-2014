namespace SCI.BL.Entities
{
    public class TextWallEntry : WallEntry
    {
        public string Content { get; set; }
        public override string Type { get { return "text"; } }
    }
}
