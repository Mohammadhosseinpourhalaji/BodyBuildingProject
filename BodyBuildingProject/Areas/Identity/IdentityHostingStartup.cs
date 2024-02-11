using System;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using BodyBuildingProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BodyBuildingProject.Areas.Identity.IdentityHostingStartup))]
namespace BodyBuildingProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DBBodyBuilding>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DBBodyBuildingConnection")));

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DBBodyBuilding>();

                services.ConfigureApplicationCookie(x =>
                {
                    x.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = y => {
                            y.Response.Redirect("/Account/Login");
                            return Task.CompletedTask;
                        }
                    };
                });

                services.Configure<IdentityOptions>(x =>
                {
                    x.Password.RequireDigit = false;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequiredUniqueChars = 0;
                    x.Password.RequiredLength = 4;
                    x.User.RequireUniqueEmail = true;
                    x.Lockout.AllowedForNewUsers = true;
                    x.Lockout.MaxFailedAccessAttempts = 3;
                    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

                });

                services.AddAuthorization(x =>
                {
                    x.AddPolicy("PolicyAdmins", p => p.RequireRole("Admins"));
                    x.AddPolicy("PolicyTrainers", p => p.RequireRole("Trainers","Admins"));
                });

            });
        }
    }
}