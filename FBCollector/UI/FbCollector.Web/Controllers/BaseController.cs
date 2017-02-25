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