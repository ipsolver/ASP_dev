using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    public class UserCategory
    {
        [Key]
        public int Id { get; set; }

        public long UsersID { get; set; }
        public Users User { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
