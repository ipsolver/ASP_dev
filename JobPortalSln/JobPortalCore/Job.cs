using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public float? Salary { get; set; }

        public string? Location { get; set; }

        [Required]
        public int CompanyID { get; set; }

        [Required]
        public int EmployerID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        [RegularExpression("Full-time|Part-time|Contract|Internship|Remote")]
        public string JobType { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        [RegularExpression("Active|Closed")]
        public string Status { get; set; } = "Active";

        public Category Category { get; set; }
    }
}
