using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.DAL.Repositries.Interfaces
{
    public interface IVideoRepositry : IGenericRepositry<Video , int>
    {
       // Task<List<ResultDetails>> GetAllVideoResultsAsync(int video_id);
  


    }
}
