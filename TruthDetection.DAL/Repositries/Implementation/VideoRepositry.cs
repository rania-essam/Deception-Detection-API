using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.DAL.Repositries.Implementation
{
    public class VideoRepositry : GenericRepository<Video, int>  , IVideoRepositry
    {
        public VideoRepositry(TruthDetectionContext _context) : base(_context)
        {
        }

        //public async Task<List<ResultDetails>> GetAllVideoResultsAsync(int video_id)
        //{
        //    var video = await _dbSet.Include(v => v.Results).FirstOrDefaultAsync(v => v.ID == video_id); 

        //    if(video == null)
        //    {
        //        throw new KeyNotFoundException($"Video with ID {video_id} not found ");
        //    }

        //    return  video.Results.ToList();
        //}




        
    }
}
