
namespace JobPortal.Models
{
        public interface IJobPortalRepository
        {
            IQueryable<Users> Users { get; }
            IQueryable<Job> Jobs { get; }
            IQueryable<Category> Categories { get; }
        IQueryable<Application> Applications { get; }

    }
}
