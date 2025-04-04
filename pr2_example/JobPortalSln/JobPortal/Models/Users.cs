using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public class Users
    {
        public long? UsersID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        public int Password { get; set; }
        public string Login { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; } = String.Empty;
        public string City { get; set; } = string.Empty;

        public string Type_access { get; set; }
        public int? CompanyID { get; set; }
        
    }
}
