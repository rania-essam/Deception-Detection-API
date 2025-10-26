using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Migrations;
using TruthDetection.DAL.Repositries;

namespace TruthDetection.DAL.Data.DbHelper
{
    public class TruthDetectionContext : IdentityDbContext<ApplicationUser>
    {

       
       public TruthDetectionContext(DbContextOptions<TruthDetectionContext> options) : base(options) { }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>( entity =>
            {
                entity.HasKey(ur => new { ur.RoleID, ur.NationalID });

            });

            // Delete User =⇒ Delete Video =⇒ Delete its Results =⇒ Delete Result Details 
            modelBuilder.Entity<ApplicationUser>()
                   .HasMany(u => u.uservideos)
                   .WithOne(v => v.User)
                   .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>()
                .HasMany(v => v.Results)
                .WithOne(r => r.video)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasMany(r => r.Details)
                .WithOne(d => d.result)
                .OnDelete(DeleteBehavior.Cascade);


            // Apply a global query filter to exclude soft-deleted entities
            //   modelBuilder.Entity<ApplicationUser>().HasQueryFilter(user => ! user.IsDeleted); // only include non deleted entities

        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ResultDetails> ResultDetails { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Result> Result { get; set; }

   







    }
}
