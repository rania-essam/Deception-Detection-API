using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.BLL.Dtos;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.BLL.Services.Interfaces
{
    public interface IAuthService
    {
       Task<AuthModel> RegisterAsync(RegisterDto registerDto);

        Task<AuthModel> GetTokenAsync(TokenRequestModel model);

        Task<string> AddRoleAsync(AddRoleModel model);
    }
}
