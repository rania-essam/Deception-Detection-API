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
    public class RoleRepositry : GenericRepository<Role, int> ,  IRoleRepositry
    {
        public RoleRepositry(TruthDetectionContext _context) : base(_context)
        {
        }


    }
}
