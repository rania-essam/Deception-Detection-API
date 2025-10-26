using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.BLL.Dtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z’_,-]+$", ErrorMessage = "First name can only  include letters,hyphen (-), underscore (_), apostrophe (’), and comma (,) ")]
        public string first_name { get; set; }

      
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z’_,-]+$", ErrorMessage = "Last name can only  include letters,  hyphen (-), underscore (_), apostrophe (’), and comma (,) ")]

        public string last_name { get; set; }

       
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Last name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[A-Za-z’_,-]+$", ErrorMessage = "Last name can only include letters, hyphen (-), underscore (_), apostrophe (’), and comma (,)  ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{14}$", ErrorMessage = "NationalID must be exactly 14 digits.")]

        public string Nid { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }

        [Required, StringLength(100)]
        public string ConfirmPassword { get; set; }

    }
}
