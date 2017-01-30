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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public JsonResult GetAllUsersFiltered(UserSearchModel model)
        {
            var users = _userService.GetAllUsersFiltered(model, User);

            return Json(users);
        }

        [HttpPost]
        public JsonResult CreateUser(UserModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            try
            {
                _userService.CreateUser(model);
                return Json(true);
            }
            catch (ApplicationException aex)
            {
                ModelState.AddModelError(string.Empty, aex.Message);
                throw new InvalidModelStateException(ModelState);
            }
        }

        [HttpPost]
        public JsonResult UpdateUser(UserModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            try
            {
                _userService.UpdateUser(model);
                return Json(true);
            }
            catch (ApplicationException aex)
            {
                ModelState.AddModelError(string.Empty, aex.Message);
                throw new InvalidModelStateException(ModelState);
            }
        }

        [HttpPost]
        public JsonResult ToggleActiveUser(int userId, bool value)
        {
            _userService.ToggleActiveUser(userId, value);
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId, User);
            return Json(user);
        }

        [HttpPost]
        public JsonResult GetActiveUsersFiltered(string searchText)
        {
            var users = _userService.GetActiveUsersFiltered(searchText);
            return Json(users);
        }

        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelStateException(ModelState);

            try
            {
                _userService.ChangePassword(model);
                return Json(true);
            }
            catch (ApplicationException aex)
            {
                ModelState.AddModelError(string.Empty, aex.Message);
                throw new InvalidModelStateException(ModelState);
            }
        }
	}
}