using Microsoft.AspNetCore.Identity;

namespace JobPortal.Models
{
    public class Applications
    {
        public int Id { get; set; }


        public int JobId { get; set; }
        public Job Job { get; set; } = default!;


        public int ApplicantId { get; set; }
        public IdentityUser Aplicant { get; set; } = default!;

        public string? CVPath { get; set; }
        public string? Status { get; set; }

    }
}
