using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FbCollector.Intefraces;
using FbCollector.Models;
using FbCollector.Web.Security;

namespace FbCollector.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILocalizationService _localizationService;

        public BaseController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        [AllowAnonymous]
        public JsonResult LoadLanguage()
        {
            var langCode = string.Empty;
            var langCookie = Request.Cookies["currentLanguage"];
            if (langCookie == null)
            {
                var lang = new HttpCookie("currentLanguage", langCode);
                Response.Cookies.Add(lang);
                return Json(true);
            }
            if (langCookie.Value != langCode)
            {
                Response.Cookies["currentLanguage"].Value = langCode;
                return Json(true);
            }

            var language = _localizationService.LoadLanguage(langCode);
            return Json(language);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult ChangeLanguage(string langCode)
        {
            var langCookie = Request.Cookies["currentLanguage"];
            if (langCookie == null)
            {
                var lang = new HttpCookie("currentLanguage", langCode);
                Response.Cookies.Add(lang);
                return Json(true);
            }
            if (langCookie.Value != langCode)
            {
                Response.Cookies["currentLanguage"].Value = langCode;
                return Json(true);
            }
            return Json(false);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetSupportedLanguages()
        {
            var languages = _localizationService.GetSupportedLanguages();
            var langCookie = Request.Cookies["currentLanguage"];

            foreach (var language in languages)
            {
                language.IsCurrent = (langCookie != null && langCookie.Value == language.Code);
            }

            return Json(languages);
        }

        protected new virtual CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }

        protected virtual bool IsAuthenticated
        {
            get { return User != null && User.Identity.IsAuthenticated; }
        }

        [HttpPost]
        public JsonResult GetImportanceLevels()
        {
            var levels = new List<KeyValuePair<int, string>>
                {
                    new KeyValuePair<int, string>(ImportanceLevel.High, "HIGH"),
                    new KeyValuePair<int, string>(ImportanceLevel.Medium, "MEDIUM"),
                    new KeyValuePair<int, string>(ImportanceLevel.Low, "LOW")
                };
            return Json(levels);
        }
    }
}