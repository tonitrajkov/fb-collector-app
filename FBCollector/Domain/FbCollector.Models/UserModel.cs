using System.Collections.Generic;

namespace FbCollector.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string ProfilePicture { get; set; }

        public bool Active { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Telephone { get; set; }

        public List<RoleModel> Roles { get; set; }
    }
}
