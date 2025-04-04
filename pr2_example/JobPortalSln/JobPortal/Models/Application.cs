using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        [Required]
        public int JobID { get; set; }

        [Required]
        public long CandidateID { get; set; }

        public string CoverLetter { get; set; }
        public string Resume { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";

        [ForeignKey("JobID")]
        public Job Job { get; set; }

        [ForeignKey("CandidateID")]
        public Users Candidate { get; set; }
    }
}
