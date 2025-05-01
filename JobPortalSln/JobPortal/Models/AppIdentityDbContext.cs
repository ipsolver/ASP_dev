using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Models
{
        public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
        {
            public AppIdentityDbContext(
            DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }
        }
    }
