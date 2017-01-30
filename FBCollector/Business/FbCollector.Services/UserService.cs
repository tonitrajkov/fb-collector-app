using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using FbCollector.Domain;
using FbCollector.Domain.Mapper;
using FbCollector.Infrastructure;
using FbCollector.Infrastructure.Helpers;
using FbCollector.Intefraces;
using FbCollector.Models;
using Microsoft.Practices.ServiceLocation;
using NHibernateCfg;

namespace FbCollector.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService()
        {
            _userRepository = ServiceLocator.Current.GetInstance<IRepository<User>>();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users =
                _userRepository.GetAll()
                    .Select(
                        u => u.ToModel());

            return users;
        }

        public UserModel GetUser(int userId)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
                throw new FbException("ITEM_DOESNT_EXIST");

            return user.ToModel();
        }

        public UserModel LoginUser(LoginModel model)
        {
            var user =
                _userRepository.Query()
                    .FirstOrDefault(
                        u => u.UserName.ToLower() == model.UserName.ToLower());

            LoginValidation(model.Password, user);

            return user.ToModel();
        }

        public void CreateUser(UserModel model)
        {
            if (_userRepository.Query().Any(u => u.UserName.ToLower() == model.UserName.ToLower()))
                throw new FbException("USER_WITH_SAME_USERNAME");

            var user = new User(model.FullName, model.UserName, model.Email, true)
            {
                Address = model.Address,
                City = model.City,
                State = model.State,
                ProfilePicture = model.ProfilePicture
            };

            var password = Internals.PasswordGenerator(8);

            user.Password = Internals.GenerateHash(password);

            if (model.Roles != null && model.Roles.Count > 0)
            {
                user.Roles = new List<Role>();

                foreach (var role in model.Roles)
                {
                    var r = new Role(role.Title) { Id = role.Id };
                    user.Roles.Add(r);
                }
            }

            _userRepository.Save(user);
        }

        public void UpdateUser(UserModel model)
        {
            var user = _userRepository.Get(model.Id);

            if (user != null)
            {
                user.Active = model.Active;
                user.Address = model.Address;
                user.City = model.City;
                user.DateModified = DateTime.Now;
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.State = model.State;
                user.Telephone = model.Telephone;
                user.ProfilePicture = model.ProfilePicture;

                if (model.Roles != null && model.Roles.Count > 0)
                {
                    user.Roles = new List<Role>();

                    foreach (var role in model.Roles)
                    {
                        var r = new Role(role.Title) { Id = role.Id };
                        user.Roles.Add(r);
                    }
                }

                _userRepository.Update(user);
            }
        }

        public SearchResult<UserModel> GetAllUsersFiltered(UserSearchModel model, IPrincipal principal)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.ItemsPerPage > 50)
            {
                model.ItemsPerPage = 50;
            }

            if (model.ItemsPerPage < 0)
            {
                model.ItemsPerPage = 5;
            }

            var query = _userRepository.Query();

            Expression<Func<User, bool>> filter = PredicateBuilder.True<User>();

            if (!string.IsNullOrEmpty(model.SearchText))
            {
                filter = filter.And(x => x.Email.ToLower().Contains(model.SearchText.Trim().ToLower()))
                               .Or(x => x.UserName.ToLower().Contains(model.SearchText.Trim().ToLower()))
                               .Or(x => x.FullName.ToLower().Contains(model.SearchText.Trim().ToLower()));
            }

            if (model.Role != null)
            {
                filter = filter.And(x => x.Roles.Any(r => r.Id == model.Role.Id));
            }

            if (model.Active.HasValue)
            {
                filter = filter.And(x => x.Active.Equals(model.Active.Value));
            }

            query = query.Where(filter);

            var totalItems = query.Count();

            var start = (model.CurrentPage - 1) * model.ItemsPerPage;
            var finalQuery = query.OrderBy(x => x.UserName).Skip(start).Take(model.ItemsPerPage);

            var result = new SearchResult<UserModel>
            {
                TotalItems = totalItems,
                Items = new List<UserModel>()
            };

            foreach (var item in finalQuery)
            {
                result.Items.Add(item.ToModel());
            }

            return result;
        }

        public LoggedInUserInfo GetLoggedInUserInfo(IPrincipal principal)
        {
            var user =
                _userRepository
                    .Query().FirstOrDefault(
                        x => x.UserName.ToLower() == principal.Identity.Name.ToLower());

            if (user == null)
                throw new FbException("INVALID_USER");

            var model = user.ToLoggedInUserModel();

            //model.IsAdmin = AuthHelper.IsAdmin(principal);
            //model.IsAssistant = AuthHelper.IsAssistant(principal);
            //model.IsStudent = AuthHelper.IsStudent(principal);
            //model.IsTeacher = AuthHelper.IsTeacher(principal);

            return model;
        }

        public void ToggleActiveUser(int userId, bool value)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
                throw new FbException("USER_NOT_FOUND");

            user.Active = value;

            _userRepository.Update(user);
        }

        public string GetUserEmailByUsername(string username)
        {
            var user =
                _userRepository.Query()
                    .FirstOrDefault(
                        u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
                throw new FbException("WRONG_USERNAME");

            if (!user.Active)
                throw new FbException("NOT_ACTIVE");

            return user.Email;
        }

        public LoggedInUserInfo GetUserById(int userId, IPrincipal principal)
        {
            var user =
                _userRepository
                    .Query().FirstOrDefault(
                        x => x.Id == userId);

            if (user == null)
                throw new FbException("ITEM_DOESNT_EXIST");

            var model = user.ToLoggedInUserModel();

            //model.IsAdmin = AuthHelper.IsAdmin(principal);
            //model.IsAssistant = AuthHelper.IsAssistant(principal);
            //model.IsStudent = AuthHelper.IsStudent(principal);
            //model.IsTeacher = AuthHelper.IsTeacher(principal);
            model.IsMyProfile = (model.UserName.ToLower() == principal.Identity.Name.ToLower());

            return model;
        }

        public IEnumerable<LoggedInUserInfo> GetActiveUsersFiltered(string searchText)
        {
            var query = _userRepository.Query()
                            .Where(u => (u.Email.ToLower().Contains(searchText.Trim().ToLower()) ||
                                        u.UserName.ToLower().Contains(searchText.Trim().ToLower()) ||
                                        u.FullName.ToLower().Contains(searchText.Trim().ToLower()))
                                        && u.Active);

            return query.Select(u => u.ToLoggedInUserModel());
        }

        public void ChangePassword(ChangePasswordModel model)
        {
            var user =
                _userRepository.Query()
                    .FirstOrDefault(
                        u => u.Id == model.UserId);

            if (user == null)
                throw new FbException("USER_NOT_FOUND");

            var pass = Internals.GenerateHash(model.OldPassword);

            if (!Internals.CompareByteArrays(pass, user.Password))
                throw new FbException("WRONG_PASSWORD");

            if (model.NewPassword.ToLower() != model.ReTypeNew.ToLower())
                throw new FbException("NEW_PASSWORD_DOESNT_MATCH");

            var newPass = Internals.GenerateHash(model.NewPassword);

            user.Password = newPass;
            _userRepository.Update(user);
        }

        public void ChangePassword(NewPasswordRequestModel model)
        {
            if (model.NewPassword.ToLower() != model.ReTypeNew.ToLower())
                throw new FbException("NEW_PASSWORD_DOESNT_MATCH");

            var user =
                _userRepository.Query()
                    .FirstOrDefault(u => u.ChangePasswordToken.ToLower() == model.Token.ToLower());

            if (user == null)
                throw new FbException("TOKEN_ISNT_VALID");

            user.ChangePasswordToken = null;
            user.TokenExpireTime = null;
            _userRepository.Update(user);

            if (user.TokenExpireTime.HasValue && (user.TokenExpireTime.Value < DateTime.Now))
                throw new FbException("TOKEN_EXPIRED");

            user.Password = Internals.GenerateHash(model.NewPassword);
            _userRepository.Update(user);
        }

        public void NewPasswordRequest(string username)
        {
            var user = _userRepository.Query()
                        .FirstOrDefault(u => u.UserName.ToLower() == username.ToLower());

            if (user == null)
                throw new FbException("WRONG_USERNAME");

            if (!user.Active)
                throw new FbException("NOT_ACTIVE");

            if (string.IsNullOrEmpty(user.Email))
                throw new FbException("EMAIL_DOESNT_EXIST");

            user.ChangePasswordToken = Internals.Base64Encode(DateTime.Now.Ticks.ToString());
            user.TokenExpireTime = DateTime.Now.AddDays(1);

            _userRepository.Update(user);
        }

        public IEnumerable<LoggedInUserInfo> GetActiveUsersWithoutAdmins(string searchText)
        {
            var query = _userRepository.Query()
                            .Where(u => (u.Email.ToLower().Contains(searchText.Trim().ToLower()) ||
                                        u.UserName.ToLower().Contains(searchText.Trim().ToLower()) ||
                                        u.FullName.ToLower().Contains(searchText.Trim().ToLower()))
                                        && u.Active && u.Roles.All(r => r.Id != 1));

            return query.OrderBy(u => u.UserName)
                    .Select(u => u.ToLoggedInUserModel());
        }

        #region Private methods

        private void LoginValidation(string password, User user)
        {
            if (user == null)
            {
                throw new FbException("WRONG_USERNAME");
            }

            if (!user.Active)
            {
                throw new FbException("NOT_ACTIVE");
            }

            var pass = Internals.GenerateHash(password);

            if (!Internals.CompareByteArrays(pass, user.Password))
            {
                throw new FbException("WRONG_PASSWORD");
            }
        }

        #endregion
    }
}
