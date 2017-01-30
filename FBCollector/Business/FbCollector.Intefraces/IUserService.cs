using System.Collections.Generic;
using System.Security.Principal;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();

        UserModel GetUser(int userId);

        UserModel LoginUser(LoginModel model);

        void CreateUser(UserModel model);

        void UpdateUser(UserModel model);

        SearchResult<UserModel> GetAllUsersFiltered(UserSearchModel model, IPrincipal principal);

        LoggedInUserInfo GetLoggedInUserInfo(IPrincipal principal);

        void ToggleActiveUser(int userId, bool value);

        string GetUserEmailByUsername(string username);

        LoggedInUserInfo GetUserById(int userId, IPrincipal principal);

        IEnumerable<LoggedInUserInfo> GetActiveUsersFiltered(string searchText);

        void ChangePassword(ChangePasswordModel model);

        void ChangePassword(NewPasswordRequestModel model);

        void NewPasswordRequest(string username);

        IEnumerable<LoggedInUserInfo> GetActiveUsersWithoutAdmins(string searchText);
    }
}
