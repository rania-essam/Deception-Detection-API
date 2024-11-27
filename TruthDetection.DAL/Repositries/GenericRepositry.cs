using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Repositries
{
    public class GenericRepositry<T> : IGenericRepositry<T> where T : class
    {
        public Task AddAsync(T Entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string nationalid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string nationalid)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T Entity)
        {
            throw new NotImplementedException();
        }
    }
}
