using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.BLL.Managers.Interfaces
{
    public interface IVideoService
    {
        Task AddResultAsync(int videoid, bool DetectionResult);
        Task GetResultAsync();

        Task UpdateResultAsync();

        Task DeleteResultAsync();
    }
}
