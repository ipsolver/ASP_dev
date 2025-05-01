using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Users
    {
        public long? UsersID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Password must be a 4-digit number")]
        public int Password { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public string Description { get; set; } = String.Empty;
        public string City { get; set; } = string.Empty;

        [Required]
        public string Type_access { get; set; }

        public int? CompanyID { get; set; }

    }
}
