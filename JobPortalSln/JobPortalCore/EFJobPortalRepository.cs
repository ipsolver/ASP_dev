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




        /// ///////////////////////////////////////////////////////
        public void CreateUser(Users user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(Users user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void DeleteUser(Users user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        /////////////////////////////////////////////////////////

        public void CreateApplication(Application app)
        {
            context.Applications.Add(app);
            context.SaveChanges();
        }

        public void UpdateApplication(Application app)
        {
            context.Applications.Update(app);
            context.SaveChanges();
        }

        public void DeleteApplication(Application app)
        {
            context.Applications.Remove(app);
            context.SaveChanges();
        }


    }
}
