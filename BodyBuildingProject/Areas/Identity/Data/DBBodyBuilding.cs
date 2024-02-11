using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BodyBuildingProject.Models
{
    public class DBBodyBuilding : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TrainSchedule> Tbl_TrainSchedule { get; set; }
        public DbSet<UserTrainInfo> Tbl_UserTrainInfo { get; set; }
        public DbSet<UserTrainSchedule> Tbl_UserTrainSchedule { get; set; }

        public DbSet<DaysOfWeek> Tbl_DaysOfWeek { get; set; }
        public DbSet<TrainType> Tbl_TrainType { get; set; }
        public DbSet<Product> Tbl_Product { get; set; }
        public DbSet<Users> Tbl_Users { get; set; }
        

        public DBBodyBuilding(DbContextOptions<DBBodyBuilding> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
