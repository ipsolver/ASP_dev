using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;


namespace JobPortal.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            JobPortalDBContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<JobPortalDBContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                new Users
                {
                    Login="vader",
                    Password=123,
                    Name = "Vadim",
                    PhoneNumber="0981212564",
                    Description = "I am student",
                    City = "Zhytomyr",
                    Type_access = "Employer"
                },
                new Users
                {
                    Login="manager",
                    Password=123,
                    Name = "Alex",
                    PhoneNumber="0971213544",
                    Description = "I am manager",
                    City = "Zhytomyr",
                    Type_access = "Manager"
                },
                new Users
                {
                    Login="admin",
                    Password=123,
                    Name = "Admin",
                    PhoneNumber="0673313556",
                    Description = "I am admin",
                    City = "Zhytomyr",
                    Type_access = "Admin"
                }

                );
                context.SaveChanges();


            }
        }
    }
}
