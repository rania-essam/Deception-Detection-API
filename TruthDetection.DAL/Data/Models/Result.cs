using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class Result : ISoftDeleted
    {
        public int ID { get; set; }



        public bool DetectionResult { get; set; }// true ==> liar 


        [ForeignKey(nameof(Video))]
        public int VideoID { get; set; }

        public Video video { get; set; }

        public ICollection<ResultDetails> Details { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
