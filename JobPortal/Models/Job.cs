using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class Job
    {
        public int Id { get; set; }

        public string EmployerId { get; set; } = string.Empty;
        public AppUser Employer { get; set; } = default!;


        public ICollection<Message> Messages { get; set; } = [];



        [Required(ErrorMessage = "Zadej název.")]
        [StringLength(40, ErrorMessage ="Maximálně 40 znaků.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Zadej firmu.")]
        [StringLength(40, ErrorMessage = "Maximálně 40 znaků.")]
        public string Company { get; set; } = string.Empty;
        [Required(ErrorMessage = "Zadej popis.")]
        [StringLength(100, ErrorMessage = "Maximálně 100 znaků.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Zadej místo.")]
        [StringLength(100, ErrorMessage = "Maximálně 40 znaků.")]
        public string Location { get; set; } = string.Empty;
        [Required(ErrorMessage = "Zadej mzdu.")]
        [Range(5000, 500000, ErrorMessage = "V rozsahu 5000 - 500 000Kč")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int Salary { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime DatePosted { get; set; }
    }
}
