using JobPortal.Models;

namespace JobPortal.Repositories
{
    public interface IApplicationRepository
    {
        void SaveApplication(Application application);
    }
}
