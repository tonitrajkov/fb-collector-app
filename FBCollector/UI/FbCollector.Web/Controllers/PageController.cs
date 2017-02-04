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
    public class PageController : BaseController
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
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
    }
}