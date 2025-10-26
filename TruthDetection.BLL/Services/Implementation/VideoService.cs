using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.BLL.Managers.Interfaces;
using TruthDetection.DAL.Repositries.Interfaces;
using TruthDetection.DAL.Data.Models;
namespace TruthDetection.BLL.Managers.Implementation
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepositry _videoRepositry;
        private readonly IResultRepo _resultRepo;
        public VideoService(IVideoRepositry _videoRepositry , IResultRepo resultRepo)
        {
            this._videoRepositry = _videoRepositry;
            this._resultRepo = resultRepo;
        }
        public async Task AddResultAsync(int videoid, bool DetectionResult)
        {
            var video = await _videoRepositry.GetByIdAsync(videoid);

            if (video == null)
                throw new Exception("Video Doesn’t Exist ! ");
            Result result = new Result
            {
                VideoID=videoid,
                DetectionResult=DetectionResult
            };
            await _resultRepo.AddAsync(result);

            await _resultRepo.SaveChangesAsync();

        }

        //public Task DeleteVideoResultAsync()
        //{
            
        //}

        //public Task GetVideoResultsAsync()
        //{
           
        //}

        //public Task UpdateResultAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
