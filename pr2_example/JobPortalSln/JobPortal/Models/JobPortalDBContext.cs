using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Models
{
    public class JobPortalDBContext : DbContext
    {
        public JobPortalDBContext(DbContextOptions<JobPortalDBContext> options) : base(options) { }
        public DbSet<Users> Users => Set<Users>();
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany()
                .HasForeignKey(a => a.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Candidate)
                .WithMany()
                .HasForeignKey(a => a.CandidateID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
