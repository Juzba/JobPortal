using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{
    public class AppUser :IdentityUser
    {
        public ICollection<Message> MessagesSent { get; set; } = [];
        public  ICollection<Job> CreatedJobs { get; set; } = [];
    }
}
