using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.DAL.Repositries.Interfaces
{
    public interface IGenericRepositry<TEntity,TKey> where TEntity : class   // any class can inherit from it
    {

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);


        Task AddAsync(TEntity Entity);

        Task SoftDeleteAsync(TEntity Entity);

        Task DeleteAsync(TKey ID);
        Task DeleteAllAsync();

        Task UpdateAsync(TEntity Entity);

        Task SaveChangesAsync();

    }
}
