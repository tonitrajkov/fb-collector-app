namespace FbCollector.Models
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ReTypeNew { get; set; }
    }

    public class NewPasswordRequestModel
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }

        public string ReTypeNew { get; set; }
    }
}
