using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

        [StringLength(500)]
        public string CoverLetter { get; set; }
        public string Resume { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";


        [NotMapped]
        public long UsersID { get; set; }



        [ForeignKey("JobID")]
        [ValidateNever]
        public Job Job { get; set; }

        [ForeignKey("CandidateID")]
        [ValidateNever]
        public Users Candidate { get; set; }
    }
}
