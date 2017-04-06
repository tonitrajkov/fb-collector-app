using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;

namespace FbCollector.Web.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;
        private readonly IFacebookService _facebookService;

        public PageController(IPageService pageService, IFacebookService facebookService)
        {
            _pageService = pageService;
            _facebookService = facebookService;
        }

        [HttpPost]
        public JsonResult GetPagesFiltered(PageSearchModel model)
        {
            var pages = _pageService.GetPagesFiltered(model);

            return Json(pages);
        }

        [HttpPost]
        public JsonResult CreatePage(PageModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            try
            {
                _pageService.CreatePage(model);
                return Json(true);
            }
            catch (ApplicationException aex)
            {
                ModelState.AddModelError(string.Empty, aex.Message);
                throw new InvalidModelStateException(ModelState);
            }
        }

        [HttpPost]
        public JsonResult UpdatePage(PageModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            try
            {
                _pageService.UpdatePage(model);
                return Json(true);
            }
            catch (ApplicationException aex)
            {
                ModelState.AddModelError(string.Empty, aex.Message);
                throw new InvalidModelStateException(ModelState);
            }
        }

        [HttpPost]
        public JsonResult DeletePage(int pageId)
        {
            _pageService.DeletePage(pageId);
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetPageById(int pageId)
        {
            var page = _pageService.GetPageById(pageId);
            return Json(page);
        }

        [HttpPost]
        public JsonResult GetPageDetails(string pageUrlId)
        {
            if (string.IsNullOrEmpty(pageUrlId))
                throw new FbException("PAGE_URLID_IS_REQURED");

            var args =
                "fields=id,about,category,checkins,cover,description,founded,likes,link,name,username,talking_about_count,website";
            var pageDetails = _facebookService.GetPageDetails(pageUrlId, args);

            return Json(pageDetails);
        }

        [HttpPost]
        public JsonResult GetPageFansByCounty(string pageUrlId)
        {
            if (string.IsNullOrEmpty(pageUrlId))
                throw new FbException("PAGE_URLID_IS_REQURED");

            var result =_facebookService.GetPageFansByCounty(pageUrlId);

            return Json(result);
        }
    }
}