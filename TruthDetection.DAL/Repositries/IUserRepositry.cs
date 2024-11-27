using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Repositries
{
    public interface IUserRepositry 
    {
        Task SoftDeleteUserAsync(string nationaid);
    }
}
