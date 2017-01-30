using System.Web.Mvc;
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
	}
}