using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class Role : ISoftDeleted
    {
        public int ID { get; set; }


        public string Name { get; set; }


        // Relations

        public ICollection<UserRole>? userRoles { get; set;  }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
