using JobPortal.Models;

namespace JobPortal.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly JobPortalDBContext _context;

        public ApplicationRepository(JobPortalDBContext context)
        {
            _context = context;
        }

        public void SaveApplication(Application application)
        {
            _context.Applications.Add(application);
            _context.SaveChanges();
        }
    }
}
