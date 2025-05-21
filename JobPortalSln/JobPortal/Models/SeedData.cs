using Microsoft.EntityFrameworkCore;

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

            //context.Jobs.RemoveRange(context.Jobs);
            //context.Categories.RemoveRange(context.Categories);
            //context.Users.RemoveRange(context.Users);
            //context.SaveChanges();
            if (5>6)
            {
                var vadim = new Users
                {
                    Login = "vader",
                    Password = 123,
                    Name = "Vadim",
                    LastName = "Lish",
                    PhoneNumber = "0981212564",
                    Description = "I am student",
                    City = "Zhytomyr",
                    Type_access = "Employer"
                };

                var alex = new Users
                {
                    Login = "manager",
                    Password = 123,
                    Name = "Alex",
                    LastName = "Rom",
                    PhoneNumber = "0971213544",
                    Description = "I am manager",
                    City = "Zhytomyr",
                    Type_access = "Manager"
                };

                var admin = new Users
                {
                    Login = "admin",
                    Password = 123,
                    Name = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "0673313556",
                    Description = "I am admin",
                    City = "Zhytomyr",
                    Type_access = "Admin"
                };

                context.Users.AddRange(vadim, alex, admin);
                context.SaveChanges();

                var categories = new[]
                {
                new Category { CategoryName = "IT, комп'ютери" },
                new Category { CategoryName = "Бухгалтерія, аудит" },
                new Category { CategoryName = "Готельно-ресторанний бізнес, туризм" },
                new Category { CategoryName = "Дизайн, творчість" },
                new Category { CategoryName = "ЗМІ, видавництво" },
                new Category { CategoryName = "Краса, фітнес, спорт" },
                new Category { CategoryName = "Нерухомість" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();

                var managerId = (int)alex.UsersID.Value;
                var companyId = 1;

                context.Jobs.AddRange(
                    new Job
                    {
                        Title = "Junior .NET Developer",
                        Description = "Робота з ASP.NET Core, C#",
                        Salary = 18000,
                        Location = "Київ",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[0].CategoryID,
                        JobType = "Full-time",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "System Administrator",
                        Description = "Підтримка офісної інфраструктури",
                        Salary = 15000,
                        Location = "Львів",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[0].CategoryID,
                        JobType = "Full-time",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Бухгалтер",
                        Description = "Ведення фінансової звітності",
                        Salary = 17000,
                        Location = "Харків",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[1].CategoryID,
                        JobType = "Full-time",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Менеджер готелю",
                        Description = "Організація роботи персоналу",
                        Salary = 20000,
                        Location = "Одеса",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[2].CategoryID,
                        JobType = "Full-time",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Графічний дизайнер",
                        Description = "Розробка дизайну для соцмереж",
                        Salary = 16000,
                        Location = "Київ",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[3].CategoryID,
                        JobType = "Remote",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Редактор новин",
                        Description = "Створення новин для сайту",
                        Salary = 15500,
                        Location = "Львів",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[4].CategoryID,
                        JobType = "Part-time",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Фітнес-тренер",
                        Description = "Проведення групових занять",
                        Salary = 22000,
                        Location = "Житомир",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[5].CategoryID,
                        JobType = "Contract",
                        Status = "Active"
                    },
                    new Job
                    {
                        Title = "Рієлтор",
                        Description = "Продаж житлової нерухомості",
                        Salary = 25000,
                        Location = "Київ",
                        CompanyID = companyId,
                        EmployerID = managerId,
                        CategoryID = categories[6].CategoryID,
                        JobType = "Full-time",
                        Status = "Active"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
