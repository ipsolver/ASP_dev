
namespace JobPortal.Models
{
        public interface IJobPortalRepository
        {
            IQueryable<Users> Users { get; }
        }
}
