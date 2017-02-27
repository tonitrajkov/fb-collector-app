using System.Diagnostics;
using FbCollector.Intefraces;
using Microsoft.Practices.Unity;

namespace FbCollector.Services
{
    public class Configuration
    {
        [DebuggerStepThrough]
        public void Configure(IUnityContainer container)
        {
            container.RegisterType(typeof(IUserService), typeof(UserService));
            container.RegisterType(typeof(IRoleService), typeof(RoleService));
            container.RegisterType(typeof(IPageService), typeof(PageService));
            container.RegisterType(typeof(IPageFeedService), typeof(PageFeedService));
            container.RegisterType(typeof(IFacebookService), typeof(FacebookService));
            container.RegisterType(typeof(ILocalizationService), typeof(LocalizationService));
        }
    }
}
