using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class FilterModel
    {
        public int PageNumber { get; set; }
        public string? Location { get; set; }
        public string? JobPosition { get; set; }

        [Range(0, 180000, ErrorMessage ="Spodní hranice mzdy musí být v rozpětí 0 - 180000kč")]
        public int MinSalary { get; set; }

        [Range(0, 200000, ErrorMessage = "Horní hranice mzdy musí být v rozpětí 0 - 200000kč")]
        public int MaxSalary { get; set; } = 200000;
    }
}
