using System.Collections.Generic;

namespace FbCollector.Models
{
    public class FbFeedResponseModel
    {
        public List<FbFeedModel> data { get; set; }
        public FbPagingModel paging { get; set; }
    }
}
