using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;

namespace FbCollector.Web.Controllers
{
    public class PageFeedController : BaseController
    {
        private readonly IPageFeedService _pageFeedService;

        public PageFeedController(IPageFeedService pageFeedService)
        {
            _pageFeedService = pageFeedService;
        }

        [HttpPost]
        public JsonResult GetPageFeedsFiltered(PageFeedSearchModel model)
        {
            var feeds = _pageFeedService.GetPageFeedsFiltered(model);

            return Json(feeds);
        }

        [HttpPost]
        public JsonResult CreatePageFeed(List<PageFeedModel> feeds)
        {
            _pageFeedService.CreatePageFeed(feeds);

            return Json(true);
        }

        [HttpPost]
        public JsonResult DeletePageFeed(int feedId)
        {
            _pageFeedService.DeletePageFeed(feedId);
            return Json(true);
        }
	}
}