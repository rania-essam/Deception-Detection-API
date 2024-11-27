using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.Models;
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

               // entity.HasOne<ApplicationUser>()
               //.WithMany(user => user.userRoles)
               //.HasForeignKey(ur => ur.NationalID)
               //.OnDelete(DeleteBehavior.NoAction);

               // entity.HasOne<Role>()
               // .WithMany(role => role.userRoles)
               // .HasForeignKey(ur => ur.RoleID)
               // .OnDelete(DeleteBehavior.NoAction);

            }
           );

            // Apply a global query filter to exclude soft-deleted entities
         //   modelBuilder.Entity<ApplicationUser>().HasQueryFilter(user => ! user.IsDeleted); // only include non deleted entities
        }



    }
}
