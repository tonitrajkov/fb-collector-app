using System.Collections.Generic;

namespace FbCollector.Models
{
    public class LoggedInUserInfo
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

        public bool IsAdmin { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsAssistant { get; set; }
        public bool IsMyProfile { get; set; }
        public bool IsTeamLead { get; set; }
    }
}
