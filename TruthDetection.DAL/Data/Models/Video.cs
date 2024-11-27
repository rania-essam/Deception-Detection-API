using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class Video
    {


       
        public int ID { get; set; }


        public string Name { get; set; }
        public bool DetectionResult { get; set; } // true => Liar , false => truth


        [Url]
        public string URL { get; set; }

        public Guid URLID { get; set; } = Guid.NewGuid(); // automatically creates new GUID

        public DateTimeOffset? Timestamp { get; set; } // the date of adding or recording a video 




        // Relations

        [ForeignKey(nameof(ApplicationUser))]
        public string NationaId { get; set; }

        public ApplicationUser User { get; set; }


        public ICollection<ResultDetails> Results { get; set; } // one or more
    }
}
