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
        private readonly IFacebookService _facebookService;

        public PageFeedController(IPageFeedService pageFeedService, IFacebookService facebookService)
        {
            _facebookService = facebookService;
            _pageFeedService = pageFeedService;
        }

        [HttpPost]
        public JsonResult GetPageFeedsFiltered(PageFeedSearchModel model)
        {
            var feeds = _pageFeedService.GetPageFeedsFiltered(model);

            return Json(feeds);
        }

        [HttpPost]
        public JsonResult DeletePageFeed(int feedId)
        {
            _pageFeedService.DeletePageFeed(feedId);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SetFeedAsUsed(int feedId)
        {
            _pageFeedService.SetFeedAsUsed(feedId);
            return Json(true);
        }

        [HttpPost]
        public JsonResult SyncPageFeed(string pageUrlId, string accessToken)
        {
            if (string.IsNullOrEmpty(pageUrlId))
                throw new FbException("PAGE_URLID_IS_REQURED");

            if (string.IsNullOrEmpty(accessToken))
                throw new FbException("ACCESTOKEN_IS_REQURED");

            var since = string.Empty;
            var sinceDate = _pageFeedService.GetLastPageFeedDate(pageUrlId);
            if (sinceDate != null)
                since = "&since=" + sinceDate.ToString();

            var args = "feed?fields=message,id,link,type,full_picture,name,shares,updated_time,created_time" + since + "&limit=100";
            _facebookService.GetPageFeed(accessToken, pageUrlId, args);

            return Json(true);
        }

        [HttpPost]
        public JsonResult PageFeedGroupedByHourAndType(PageFeedSearchModel model)
        {
            var chartData = _pageFeedService.PageFeedGroupedByHourAndType(model);

            return Json(chartData);
        }
    }
}