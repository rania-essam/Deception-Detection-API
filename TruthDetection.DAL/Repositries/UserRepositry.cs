using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.DAL.Repositries
{
    public class UserRepositry : IUserRepositry
    {

        private readonly TruthDetectionContext _truthDetectionContext;
        public UserRepositry(TruthDetectionContext _truthDetectionContext)
        {
            this._truthDetectionContext = _truthDetectionContext;
        }
        public async Task SoftDeleteUserAsync(string nationaid)
        {
            
        }
    }
}
