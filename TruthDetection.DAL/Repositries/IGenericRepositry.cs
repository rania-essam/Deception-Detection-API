using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Repositries
{
    public interface IGenericRepositry<T> where T : class   // any class can inherit from it
    {

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(string nationalid);


        Task AddAsync(T Entity);

        Task DeleteAsync(string nationalid);

        Task UpdateAsync(T Entity);

    }
}
