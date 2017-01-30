using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FbCollector.Infrastructure.Helpers;

namespace FbCollector.Web.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentUser == null || !CurrentUser.Identity.IsAuthenticated)
            {
                throw new FbException("USER_NOT_LOGGED");
            }
        }
    }

    public class DoNotResetAuthCookieAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        }
    }
}