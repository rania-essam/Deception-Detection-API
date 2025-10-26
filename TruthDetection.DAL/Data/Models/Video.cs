using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Data.Models
{
    public class Video : ISoftDeleted
    {




        public int ID { get; set; }


        public string Name { get; set; }
        // public bool DetectionResult { get; set; } // true => Liar , false => truth


        [Url]
        public string URL { get; set; } // Generated Using Guid 


        public DateTime AddedAt { get; set; } // the date of adding or recording a video 




        // Relations  
        // Video has one "UserVideo"
        //Video has more than one result ( every 20 sec there is a result )
        // Video ID is pk , and fk in USer video table

        [ForeignKey("ApplicationUser")]
        public string UserID       { get; set; }
        public ApplicationUser User { get; set; }
       

        public ICollection<Result> Results { get; set; } // one or more




        // implement softdeletion
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}
