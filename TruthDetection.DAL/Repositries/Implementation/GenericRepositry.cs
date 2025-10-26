using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.DAL.Repositries.Implementation
{
    public class GenericRepository<TEntity, TKey> : IGenericRepositry<TEntity, TKey> where TEntity : class
    {
        protected readonly TruthDetectionContext _context;
        protected readonly DbSet<TEntity> _dbSet;

         public GenericRepository(TruthDetectionContext _context)
        {
            this._context = _context;
            _dbSet = _context.Set<TEntity>(); // Initialize the DbSet for the entity type T
        }
         public async Task<IEnumerable<TEntity>> GetAllAsync()
         {
            return await _dbSet.ToListAsync(); //  Get all entities from the DbSet ( for any type)
         }

 
         public async Task<TEntity> GetByIdAsync(TKey id)  // Find designed to work only with pks 
         {
        // Implementation goes here (e.g., querying the database)
              return await _dbSet.FindAsync(id);
         }
         public async Task AddAsync(TEntity Entity)
         {
            await _dbSet.AddAsync(Entity); // Add the entity to the DbSet
            //await _context.SaveChangesAsync(); // save changes asyncronously
         }

         public async Task SoftDeleteAsync(TEntity Entity)
         {

            if (Entity is ISoftDeleted softdeletableentity)
            {
                softdeletableentity.IsDeleted = true;
                softdeletableentity.DeletedAt = DateTime.Now;
                _dbSet.Update(Entity); // update method ( is not part of LINQ ) is a method in EF that mark the state of the Entity as "Modified" in The ChangeTracker
            }
            else
            {
                throw new InvalidOperationException("Entity doesn’t Support SoftDeletion ");
            }



         }

         public async Task UpdateAsync(TEntity Entity)
        {

            _dbSet.Update(Entity); // Mark the entity as modified in the DbSet ( changetracker)

        }

         public async Task SaveChangesAsync()
         {
            await _context.SaveChangesAsync();
         }

        public async Task DeleteAsync(TKey ID)
        {
            var user = await _context.User.FindAsync(ID);

            _context.User.Remove(user);
            
        }

        public async Task DeleteAllAsync()
        {
            var users =  _context.User.ToList(); // fetch all users 

            _context.User.RemoveRange(users);


        }
    }
}
