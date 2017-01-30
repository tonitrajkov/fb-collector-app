using System.Collections.Generic;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IRoleService
    {
        IEnumerable<RoleModel> GetRoles();
    }
}
