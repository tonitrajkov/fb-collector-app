namespace FbCollector.Models
{
    public class FbFeedModel
    {
        public string id { get; set; }
        public string created_time { get; set; }
        public string full_picture { get; set; }
        public string link { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public FbShareModel shares { get; set; }
        public string type { get; set; }
        public string updated_time { get; set; }
    }

    public class FbShareModel
    {
        public int? count { get; set; }
    }
}
