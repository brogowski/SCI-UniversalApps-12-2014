namespace SCI.BL.Entities
{
    public class ImageWallEntry : WallEntry
    {
        public string Base64Content { get; set; }
        public override string Type
        {
            get { return "image"; }
        }
    }
}
