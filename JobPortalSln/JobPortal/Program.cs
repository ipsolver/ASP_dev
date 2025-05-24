using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Identity;
using JobPortal.Hubs;
using JobPortal.Components;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JobPortalDBContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:JobPortalConnection"]);
});

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IJobPortalRepository, EFJobPortalRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

builder.Services.AddAuthorization();
builder.Services.AddSignalR();


var app = builder.Build();

app.MapHub<NotificationHub>("/notificationHub");


app.UseStaticFiles();
app.UseRouting();

app.UseSession();


app.UseAuthentication();
app.UseAuthorization();



app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
