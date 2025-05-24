using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalClient.Models
{
    public class Application
    {
        public int ApplicationID { get; set; }

        public int JobID { get; set; }

        public long CandidateID { get; set; }

        public string CoverLetter { get; set; }
        public string Resume { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";


        public long UsersID { get; set; }



        [ForeignKey("JobID")]
        public Job Job { get; set; }

        [ForeignKey("CandidateID")]
        public Users Candidate { get; set; }
    }
}
