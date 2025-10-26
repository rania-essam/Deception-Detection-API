using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; } 
        public DateTime? DeletedAt { get; set; }
    }
}
