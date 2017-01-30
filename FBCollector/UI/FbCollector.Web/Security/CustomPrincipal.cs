using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using FbCollector.Models;

namespace FbCollector.Web.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public List<RoleModel> Roles { get; set; }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new ArgumentNullException("role");

            return Roles.SingleOrDefault(x => x.Title == role) != null;
        }
    }
}