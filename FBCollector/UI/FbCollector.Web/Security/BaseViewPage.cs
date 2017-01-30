using System.Web.Mvc;

namespace FbCollector.Web.Security
{
    public abstract class BaseViewPage : WebViewPage
    {
        public new virtual CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }

    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public new virtual CustomPrincipal User
        {
            get { return base.User as CustomPrincipal; }
        }
    }
}