using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JobPortalDBContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:JobPortalConnection"]);
});

builder.Services.AddScoped<IJobPortalRepository, EFJobPortalRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
