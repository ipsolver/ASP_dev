using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalClient.Models
{
    public class Job
    {
        public int JobID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float? Salary { get; set; }

        public string? Location { get; set; }

        public int CompanyID { get; set; }

        public int EmployerID { get; set; }

        public int CategoryID { get; set; }

        [RegularExpression("Full-time|Part-time|Contract|Internship|Remote")]
        public string JobType { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Now;

        [RegularExpression("Active|Closed")]
        public string Status { get; set; } = "Active";

        public Category Category { get; set; }
    }
}
