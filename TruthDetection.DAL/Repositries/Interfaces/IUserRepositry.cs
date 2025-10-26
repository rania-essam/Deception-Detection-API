using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.DAL.Repositries.Interfaces
{
    public interface IUserRepositry : IGenericRepositry<ApplicationUser,string>
    {


        Task<List<ApplicationUser>> GetAlLlUsersAsync();
       // Task<List<Video>> GetAllVideosAsync(string national_id);
       // Task<List<UserRole>> GetAllUserRolesAsync(string national_id);

       //// Task AddVideoByUserAsync(string national_id , Video video);

       // Task AddRoleByUserAsync(string national_id , UserRole role);
    }
}
