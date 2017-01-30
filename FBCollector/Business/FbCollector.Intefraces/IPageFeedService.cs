using System.Collections.Generic;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IPageFeedService
    {
        void CreatePageFeed(List<PageFeedModel> feeds);

        void DeletePageFeed(int feedId);

        SearchResult<PageFeedModel> GetPageFeedsFiltered(PageFeedSearchModel model);
    }
}
