using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FbCollector.Intefraces;
using FbCollector.Models;
using FbCollector.Web.Security;
using Newtonsoft.Json;

namespace FbCollector.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword(string token)
        {
            if (string.IsNullOrEmpty(token))
                RedirectToAction("Login");

            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.LoginUser(model);

                    var serializeModel = new CustomPrincipalSerializeModel
                    {
                        UserId = user.Id,
                        FullName = user.FullName,
                        Roles = user.Roles
                    };

                    var userData = JsonConvert.SerializeObject(serializeModel);
                    var authTicket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        userData);

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.Name = ex.Message;
                }
            }
            else
            {
                ViewBag.Name = "USERNAME_PASSWORD_ARE_REQUIRED";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(string m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_userService.NewPasswordRequest(model.UserName);

                    ViewBag.Name = "NEW_PASSWORD_IS_SEND_ON_MAIL";
                }
                catch (Exception ex)
                {
                    ViewBag.Name = ex.Message;
                }
            }
            else
            {
                ViewBag.Name = "USERNAME_IS_REQUIRED";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            if (ModelState.IsValid)
            {
               // ViewBag.Token = model.Token;
                try
                {
                //    _userService.ChangePassword(model);

                    RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.Name = ex.Message;
                }
            }
            else
            {
                ViewBag.Name = "USERNAME_IS_REQUIRED";
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Json(true);
        }
	}
}