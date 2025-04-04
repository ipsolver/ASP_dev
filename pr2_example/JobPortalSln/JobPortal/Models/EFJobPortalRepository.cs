using Microsoft.EntityFrameworkCore;
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
        public IQueryable<Job> Jobs => context.Jobs.Include(j => j.Category);
        public IQueryable<Category> Categories => context.Categories;
        public IQueryable<Application> Applications => context.Applications;

    }
}
