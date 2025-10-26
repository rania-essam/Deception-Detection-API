using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.DAL.Repositries.Implementation
{
    public class UserRepository : GenericRepository<ApplicationUser,string> , IUserRepositry
    {
        public UserRepository(TruthDetectionContext _context): base(_context)
        {
            
        }

      

        public async Task AddRoleByUserAsync(string national_id, UserRole role)
        {

           // var user = await _dbSet.FirstOrDefaultAsync(u => u.NationalID == national_id);
            var user = await _dbSet.FirstOrDefaultAsync(u => u.NationalID == national_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with National_id {national_id} Not_Found . ");

            }
            user.userRoles.Add(role);
            await _context.SaveChangesAsync();
        }

       
        public async Task AddVideoByUserAsync(string national_id , Video video)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.NationalID == national_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with National_id {national_id} Not_Found . ");

            }
          //  user.Videos.Add(video);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRole>> GetAllUserRolesAsync(string national_id)
        {
            var user = await _dbSet.FirstOrDefaultAsync(u => u.NationalID == national_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with National_id {national_id} Not_Found . ");

            }

            var roles =  user.userRoles.ToList();

            return roles;



        }

        public async Task<List<ApplicationUser>> GetAlLlUsersAsync()
        {
            Console.WriteLine(_context.User.ToList().Count());
           return _context.User.ToList();
        }

        //public async Task<List<Video>> GetAllVideosAsync(string national_id)
        //{
        //    var user = await _dbSet.FirstOrDefaultAsync(u => u.NationalID == national_id);

        //    if (user == null)
        //    {
        //        throw new KeyNotFoundException($"User with National_id {national_id} Not_Found . ");

        //    }
        // //   var videos = user.Videos.ToList();

        //    return videos;
        //}


    }
}
