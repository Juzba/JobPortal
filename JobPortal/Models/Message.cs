using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }

        public Job? Job { get; set; }

        public string JobSeekerId { get; set; } = string.Empty;
        public IdentityUser? JobSeeker { get; set; }



    }
}
