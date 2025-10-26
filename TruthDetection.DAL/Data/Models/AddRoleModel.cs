using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class AddRoleModel
    {

        [Required]
        public string userid { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
