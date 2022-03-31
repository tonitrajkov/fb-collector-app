using System;

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

        public int? Importance { get; set; }
    }

    public class PageFeedSearchModel : SearchModel
    {
        public string PageUrlId { get; set; }

        public string SearchText { get; set; }

        public bool? IsUsed { get; set; }

        public string Type { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool OrderDescending { get; set; }

        public int? SharesNumber { get; set; }

        public int? Year { get; set; }
    }
}
