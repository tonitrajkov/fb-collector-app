using System.Collections.Generic;
using FbCollector.Models;

namespace FbCollector.Web.Security
{
    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public List<RoleModel> Roles { get; set; }
    }
}