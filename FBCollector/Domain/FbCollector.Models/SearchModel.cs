namespace FbCollector.Models
{
    public class SearchModel
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
    }

    public class UserSearchModel : SearchModel
    {
        public string SearchText { get; set; }

        public RoleModel Role { get; set; }

        public bool? Active { get; set; }
    }

    public class PageSearchModel : SearchModel
    {
        public string SearchText { get; set; }
    }

    public class PageFeedSearchModel : SearchModel
    {
        public string SearchText { get; set; }
    }
}
