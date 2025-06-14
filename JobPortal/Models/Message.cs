using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }




        public int JobId { get; set; }
        public Job Job { get; set; } = default!;



        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = default!;


    }
}
