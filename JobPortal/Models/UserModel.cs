namespace JobPortal.Models
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public IList<string>? Roles { get; set; }

        public bool Ban { get; set; }

        public DateTimeOffset BanEnds { get; set; }

    }
}
