using System.ComponentModel.DataAnnotations;

namespace JobPortalClient.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public ICollection<Job> Jobs { get; set; }
    }
}
