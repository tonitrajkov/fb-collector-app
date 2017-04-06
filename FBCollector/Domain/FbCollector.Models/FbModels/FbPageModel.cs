namespace FbCollector.Models
{
    public class FbPageModel
    {
        public string id { get; set; }
        public string about { get; set; }
        public string category { get; set; }
        public int? checkins { get; set; }
        public string description { get; set; }
        public string founded { get; set; }
       // public int? likes { get; set; }
        public string link { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public int? talking_about_count { get; set; }
        public string website { get; set; }
        public FbCoverModel cover { get; set; }
    }

    public class FbCoverModel
    {
        public string cover_id { get; set; }
        public int offset_x { get; set; }
        public int offset_y { get; set; }
        public string source { get; set; }
        public string id { get; set; }
    }
}