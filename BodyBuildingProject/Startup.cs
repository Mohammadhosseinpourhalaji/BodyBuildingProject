using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BodyBuildingProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseCookiePolicy();

            //var cultureInfo = new CultureInfo("fr-FR");
            //cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            InitRoles(serviceProvider).Wait();
        }

        private async Task InitRoles(IServiceProvider serviceProvider)
        {
            var rolemanager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var usermanager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roles = new List<string> { "Admins", "Trainers" };
            foreach (var x in roles)
            {
                if ((await rolemanager.RoleExistsAsync(x)) == false)
                {
                    var role = new IdentityRole(x);
                    await rolemanager.CreateAsync(role);
                }
            }
            //ADMIN
            var adminuser = await usermanager.FindByNameAsync("admin");
            if (adminuser == null)
            {
                adminuser = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    FirstName = "AdminFName",
                    LastName = "AdminLName"
                };
                usermanager.CreateAsync(adminuser, "password123").Wait();
            }
            if ((await usermanager.IsInRoleAsync(adminuser, "Admins")) == false)
            {
                usermanager.AddToRoleAsync(adminuser, "Admins").Wait();
            }

            //TRAINER
            var traineruser = await usermanager.FindByNameAsync("trainer");
            if (traineruser == null)
            {
                adminuser = new ApplicationUser
                {
                    Email = "trainer@gmail.com",
                    UserName = "trainer",
                    FirstName = "trainerf",
                    LastName = "trainerl"
                };
                usermanager.CreateAsync(adminuser, "password123").Wait();
            }
            if ((await usermanager.IsInRoleAsync(adminuser, "Trainers")) == false)
            {
                usermanager.AddToRoleAsync(adminuser, "Trainers").Wait();
            }
        }

    }
}
