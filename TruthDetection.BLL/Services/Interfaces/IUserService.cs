using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.BLL.Dtos;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> UploadImageAsync(AddFileDto addFileDto );
        Task<string> DeleteFile(DeleteFileDto deleteFileDto); // imagename ==> file 

        Task<IMGDto> ShowFileAsync(ShowFileDto showImageDto);

        Task<IMGDto> DownloadImageAsync(DownloadImageDto downloadImageDto);

        Task<string> DeleteVideoAsync(DeleteVideoDto deleteVideoDto);

        Task<string> UploadVideoAsync(AddFileDto addFileDto);

        Task<IMGDto> ShowVideoAsync(ShowVideoDto showVideo);
        Task<IMGDto> DownloadVideoAsync(ShowVideoDto showVideo);


        Task<userdatadto> GetUserDataAsync(string user_id);

        Task<List<userdatadto>> GetAllUsersAsync();

        Task UpdateUserAsync(UpdateUserDto updateduser);

        Task DeleteUserAsync(string userid);
        Task DeleteAllUsersAsync();


     

    }
}
