using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FbCollector.FluentValidators;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Web.Security;
using FluentValidation.Mvc;
using log4net;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using NHibernate;
using NHibernateCfg;
using Unity.Mvc5;

namespace FbCollector.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Log = LogManager.GetLogger("WEB");

        private IUnityContainer _container;
        private UnityServiceLocator _locator;
        private UnityDependencyResolver _dependencyResolver;

        private readonly object _lockObject = new object();
        private static bool _configured;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected MvcApplication()
        {
            Error += Application_Error;
            BeginRequest += MvcApplication_BeginRequest;
        }

        void MvcApplication_BeginRequest(object sender, EventArgs e)
        {
            if (!_configured)
            {
                ConfigureSystem();
            }
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket == null) return;
                var serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                var newUser = new CustomPrincipal(authTicket.Name)
                {
                    UserId = serializeModel.UserId,
                    FullName = serializeModel.FullName,
                    Roles = serializeModel.Roles
                };

                HttpContext.Current.User = newUser;
            }
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var ex = httpApplication.Context.Error;
            var error = ex as InvalidModelStateException;

            if (error != null)
            {
                var message = error.Message.Replace("model.", "");
                ReturnError(httpApplication, message, false);
                return;
            }

            if (ex is UnauthorizedAccessException)
            {
                ReturnError(httpApplication, "UNAUTHORIZED");
                return;
            }

            if (ex is FbException)
            {
                ReturnError(httpApplication, (ex as FbException).Message);
                return;
            }

            if (ex is ApplicationException)
            {
                ReturnError(httpApplication, "SYSTEM_ERROR");
                return;
            }

            if (ex is NotImplementedException)
            {
                ReturnError(httpApplication, "Not implemented");
                return;
            }

            if (ex == null) return;

            Log.Error(ex);

            ReturnError(httpApplication, "SYSTEM_ERROR");
        }

        private void ReturnError(HttpApplication application, string message, bool formatMessage = true)
        {
            if (formatMessage)
            {
                message = FormatErrorMessage(message);
            }

            application.Context.ClearError();
            application.Context.Response.TrySkipIisCustomErrors = true;
            application.Context.Response.Write(message);
            application.Context.Response.ContentType = "application/json; charset=utf-8";
            application.Context.Response.StatusCode = 409;
        }

        private string FormatErrorMessage(string message)
        {
            return "{" + string.Format("\"Message\":\"{0}\"", message) + "}";
        }

        /// <summary>
        /// System Configuration
        /// </summary>
        private void ConfigureSystem()
        {
            lock (_lockObject)
            {
                if (_configured)
                {
                    return;
                }

                try
                {
                    //Unity Container Configuration
                    _container = new UnityContainer();

                    //Configure Services in unity container
                    new Services.Configuration().Configure(_container);

                    //Set FluentValidation as ModelValidatorProvider
                    DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
                    ModelValidatorProviders.Providers.Clear();
                    //Configure fluent validator
                    FluentValidationModelValidatorProvider.Configure(p => p.ValidatorFactory = ConfigureValidators.Configure());

                    //NHibernate configuration
                    ISessionFactory factory = SessionFactory.GetSessionFactory();
                    new NhConfiguration()
                        .WithSessionFactory(_container, factory)
                        .Configure(_container);

                    _locator = new UnityServiceLocator(_container);
                    _dependencyResolver = new UnityDependencyResolver(_container);

                    ServiceLocator.SetLocatorProvider(() => _locator);
                    DependencyResolver.SetResolver(_dependencyResolver);

                    _configured = true;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);

                    _locator = null;
                    _configured = false;
                    _dependencyResolver = null;
                    ServiceLocator.SetLocatorProvider(null);
                }
            }
        }
    }
}
