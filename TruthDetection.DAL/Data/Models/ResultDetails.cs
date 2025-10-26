using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class ResultDetails : ISoftDeleted
    {
        public int ID { get; set; }
        public string Reason { get; set; }

        // relations

        [ForeignKey(nameof(Result))]
        public int ResultID { get; set; }
        public Result result{ get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }


    }
}
