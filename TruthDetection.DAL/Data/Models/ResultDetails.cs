using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class ResultDetails
    {
        public int ID { get; set; }
        public string Reason { get; set; }

        // relations

        [ForeignKey(nameof(Video))]
        public int VideoID { get; set; }
        public Video video { get; set; }


    }
}
