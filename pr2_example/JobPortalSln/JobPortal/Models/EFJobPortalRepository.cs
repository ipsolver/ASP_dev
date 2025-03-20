using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPortal.Models
{
    public class EFJobPortalRepository : IJobPortalRepository
    {
        private JobPortalDBContext context;
        public EFJobPortalRepository(JobPortalDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Users> Users => context.Users;

    }
}
