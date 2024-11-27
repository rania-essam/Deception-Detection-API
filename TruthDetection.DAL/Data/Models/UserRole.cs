using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class UserRole  // pk and fk in OnModelCreating  FUNC ( Fluent api )
    {

       
        public string NationalID { get; set; } // Foreign key for applicationuser

     
        public int RoleID { get; set; } // Foreign key for Role

       

        // Relations
        public ApplicationUser User { get; set; }
        public Role role { get; set; }
        // fk and pk in fluentapi

     
    }
}
