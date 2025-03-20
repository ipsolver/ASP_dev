using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Models
{
    public class JobPortalDBContext : DbContext
    {
        public JobPortalDBContext(DbContextOptions<JobPortalDBContext> options) : base(options) { }
        public DbSet<Users> Users => Set<Users>();

}
}
