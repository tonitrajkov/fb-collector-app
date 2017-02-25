using System.Web;
using System.Web.Mvc;
using FbCollector.Intefraces;

namespace FbCollector.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILocalizationService _localizationService;

        public HomeController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public JsonResult LoadLanguage()
        {
            var langCode = string.Empty;
            var langCookie = Request.Cookies["currentLanguage"];
            if (langCookie != null)
            {
                langCode = langCookie.Value;
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
    }
}