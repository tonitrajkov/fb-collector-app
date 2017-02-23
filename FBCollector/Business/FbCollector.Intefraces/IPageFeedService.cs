﻿using System.Collections.Generic;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IPageFeedService
    {
        void CreatePageFeed(List<FbFeedModel> feeds, string pageUrlId);

        void DeletePageFeed(int feedId);

        SearchResult<PageFeedModel> GetPageFeedsFiltered(PageFeedSearchModel model);

        void SetFeedAsUsed(int feedId);

        long? GetLastPageFeedDate(string pageUrlId);
    }
}
