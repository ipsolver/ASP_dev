using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
