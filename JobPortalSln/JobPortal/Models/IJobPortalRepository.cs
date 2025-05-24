
namespace JobPortal.Models
{
        public interface IJobPortalRepository
        {
            IQueryable<Users> Users { get; }
            IQueryable<Job> Jobs { get; }
            IQueryable<Category> Categories { get; }
            IQueryable<Application> Applications { get; }
            IQueryable<Notification> Notifications { get; }
            IQueryable<UserCategory> UserCategories { get; }

        ////////////////////////////////////////////////////////

        void CreateUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);

        void CreateApplication(Application app);
        void UpdateApplication(Application app);
        void DeleteApplication(Application app);

        ///////////////////////////////////////
        void CreateJob(Job job);
        void AddNotification(Notification notification);

        ///////////////////////////////////
        void AddUserCategory(UserCategory userCategory);
        void RemoveUserCategories(IEnumerable<UserCategory> userCategories);

        void SaveChanges();

    }
}
