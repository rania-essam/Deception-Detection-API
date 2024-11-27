using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    // Identity user has default pk ( ID GUID string )

    [Index(nameof(NationalID) , IsUnique = true )]
    public class ApplicationUser : IdentityUser
    {
        // Email & Pass from IdentityUser

        [Required]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "NationalID must be exactly 14 digits.")]
        public string NationalID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }


        [Url]
        public string ProfilePictureURL { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }

        public DateTimeOffset? LastLogin { get; set; } = DateTime.UtcNow;

    //    public bool IsDeleted { get; set; } = false;



        // Relations

        public ICollection<Video>? Videos { get; set; } // nullable => user can have zero videos 


        public ICollection<UserRole>? userRoles { get; set; } // nullable as user can be logged out 



    }
}
