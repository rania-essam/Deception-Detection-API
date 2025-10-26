using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthDetection.BLL.Dtos;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.BLL.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IWebHostEnvironment _env;
      
        private readonly IUserRepositry _userRepositry;

        private readonly IVideoRepositry _videoRepositry;

       
        public UserService(IWebHostEnvironment env,  IUserRepositry _userRepositry , IVideoRepositry _videoRepositry)
        {
            _env = env;
      
            this._userRepositry = _userRepositry;

            this._videoRepositry = _videoRepositry;
        }
        #region IMAGE
        public async Task<string> UploadImageAsync(AddFileDto addFileDto)
        {

            if (addFileDto.File == null || addFileDto.File.Length == 0 )
            {
                     throw new Exception("File is required !");
            }

            // Mime type of the file
            var contenttype = addFileDto.File.ContentType;  // imagename.jpg

            var fileextension = Path.GetExtension(contenttype).ToLower(); // .jpg

            string FolderName = "Images";
           
           
            //  Console.WriteLine(_env.WebRootPath);
            //    Console.WriteLine(FolderName);
            // 1 - Get Located Folder Path
            string folderpath = "";
            try
            {
                folderpath = Path.Combine(_env.WebRootPath, FolderName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {folderpath}");
            }
            // 2 - Get File Name , Guid adds string in the beginning to make it unique
            string FileName = $"{Guid.NewGuid()}{addFileDto.File.FileName}"; // 

            var user = await _userRepositry.GetByIdAsync(addFileDto.ID);  // get user from db
            if (user == null)  // check if user exists
                throw new Exception("User Not Found ");


            //3 - saving file name to database
         
               user.ImageName = FileName;  // save image name to database
               await _userRepositry.SaveChangesAsync(); // save changes to database
            
            // 4 - Creating File Path
            string FilePath = Path.Combine(folderpath, FileName);

            // 5 - Save File As Streams
            using (var sf = new FileStream(FilePath, FileMode.Create))
               await addFileDto.File.CopyToAsync(sf);

            return FileName;


        }

        public async Task<string> DeleteFile(DeleteFileDto deleteFileDto)
        {

            string FilePath = Path.Combine(_env.WebRootPath, deleteFileDto.FolderName, deleteFileDto.FileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }



            // delete file name from database
            var user = await _userRepositry.GetByIdAsync(deleteFileDto.ID);  // get user from db
            if (user == null)
            {
                throw new Exception("User Not Found ");
            } 
           
                user.ImageName = null;
                await _userRepositry.SaveChangesAsync();
            
          
            return "File Deleted Successfully !";


        }

        public async Task<IMGDto> ShowFileAsync(ShowFileDto showImageDto)
        {
            var user = await _userRepositry.GetByIdAsync(showImageDto.ID);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID {showImageDto.ID} not found.");
            }

            // 1 - get the path of the image in wwwroot folder

            var imagepath = Path.Combine(_env.WebRootPath , "Images", Path.GetFileName(showImageDto.ImageName));  // nameofimg = guid + imagename

          
            // 3 -  Get the file extension to determine the MIME type
            var fileExtension = Path.GetExtension(imagepath).ToLowerInvariant();

            string mimeType = fileExtension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream"  // MIME type if unknown
            };



            // 4  - read the file as bytes 
            byte[] imagebytess = await System.IO.File.ReadAllBytesAsync(imagepath);

            return new IMGDto { imagesbytes = imagebytess, MemoType = mimeType };


        }

        public async Task<IMGDto> DownloadImageAsync(DownloadImageDto downloadImageDto)
        {
            var user = await _userRepositry.GetByIdAsync(downloadImageDto.ID);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {downloadImageDto.ID} not found.");
            }

            // 1 - get the path of the image in wwwroot folder

            var imagepath = Path.Combine(_env.WebRootPath, "Images", downloadImageDto.ImageName);  // nameofimg = guid + imagename



            // 3 -  Get the file extension to determine the MIME type
            var fileExtension = Path.GetExtension(imagepath).ToLowerInvariant();

            string mimeType = fileExtension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                _ => "application/octet-stream"  //  MIME type if unknown
            };


            // 4  - read the file as bytes 
            byte[] imagebytess = await System.IO.File.ReadAllBytesAsync(imagepath);


        

            return new IMGDto { imagesbytes = imagebytess, MemoType = mimeType };

        }

        #endregion

        #region Video
        public async Task<string> DeleteVideoAsync(DeleteVideoDto deleteVideoDto)
        {
            // 1 - Get video name
            string FilePath = Path.Combine(_env.WebRootPath, "Videos", deleteVideoDto.Videoname);

            // 2 - if file exists ==> it’s deleted 
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            // 3 - delete video from database
            var video =await  _videoRepositry.GetByIdAsync(deleteVideoDto.VideoID);
            if (video == null)
            {
                throw new Exception("Video Not Found !");
            }
            await _videoRepositry.SoftDeleteAsync(video);

            await _videoRepositry.SaveChangesAsync();
            return "Video Deleted Successfully ";
        }

        public async Task<string> UploadVideoAsync(AddFileDto addFileDto)
        {
            if (addFileDto.File == null || addFileDto.File.Length == 0)
            {
                throw new Exception("File is required !");
            }

     

            string FolderName = "Videos";


            //  Console.WriteLine(_env.WebRootPath);
            //    Console.WriteLine(FolderName);
            // 1 - Get Located Folder Path
            string folderpath = "";
            try
            {
                folderpath = Path.Combine(_env.WebRootPath, FolderName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message} {folderpath}");
            }

            // 2 - Get File Name , Guid adds string in the beginning to make it unique
            string FileName = $"{Guid.NewGuid()}{addFileDto.File.FileName}"; // 

            var user = await _userRepositry.GetByIdAsync(addFileDto.ID);  // get user from db
            if (user == null)  // check if user exists
                throw new Exception("User Not Found ");

            user.UserName = $"{user.FirstName}{user.LastName}";
            //3 - saving file name to database
            var userid = addFileDto.ID;
            Video video = new Video // ID identity 
            {
                Name = user.UserName,
                URL = FileName,
                AddedAt = DateTime.Now,
                UserID=userid
                
            };
            await _videoRepositry.AddAsync(video);

            await _videoRepositry.SaveChangesAsync();

            // 4 - Creating File Path
            string FilePath = Path.Combine(folderpath, FileName);

            // 5 - Save File As Streams
            using (var sf = new FileStream(FilePath, FileMode.Create))
                await addFileDto.File.CopyToAsync(sf);

            return FileName;
        }

        public async Task<IMGDto> ShowVideoAsync(ShowVideoDto showVideoDto)
        {
            var user = await _userRepositry.GetByIdAsync(showVideoDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with  ID {showVideoDto.UserId} not found.");
            }

            // 1 - get the path of the image in wwwroot folder

            var imagepath = Path.Combine(_env.WebRootPath, "Videos", showVideoDto.VideoName);  // nameofimg = guid + imagename


            // 3 -  Get the file extension to determine the MIME type
            var fileExtension = Path.GetExtension(imagepath).ToLowerInvariant();

            string mimeType = fileExtension switch
            {
                ".mp4" => "video/mp4",
                ".mpeg" => "video/mpeg",
                ".avi" => "video/x-msvideo",  // For .avi files
                ".mkv" => "video/x-matroska", // For .mkv files
                ".webm" => "video/webm",
                ".3gp" => "video/3gpp",
                ".mov" => "video/quicktime",
                ".flv" => "video/x-flv",
                ".rm" => "application/vnd.rn-realmedia",  // For .rm files
                ".wmv" => "video/x-ms-wmv", // For .wmv files
                _ => "application/octet-stream"  // MIME type if unknown
            };


            // 4  - read the file as bytes 
            byte[] bytess = await System.IO.File.ReadAllBytesAsync(imagepath);

            return new IMGDto { imagesbytes = bytess, MemoType = mimeType };//imagesbytes ==> videobytes
        }

        public async Task<IMGDto> DownloadVideoAsync(ShowVideoDto showVideoDto)
        {
            var user = await _userRepositry.GetByIdAsync(showVideoDto.UserId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {showVideoDto.UserId} not found.");
            }

            // 1 - get the path of the image in wwwroot folder

            var path = Path.Combine(_env.WebRootPath, "Videos", showVideoDto.VideoName);  // nameofimg = guid + imagename



            // 3 -  Get the file extension to determine the MIME type
            var fileExtension = Path.GetExtension(path).ToLowerInvariant();

            string mimeType = fileExtension switch
            {
                ".mp4" => "video/mp4",
                ".mpeg" => "video/mpeg",
                ".avi" => "video/x-msvideo",  // For .avi files
                ".mkv" => "video/x-matroska", // For .mkv files
                ".webm" => "video/webm",
                ".3gp" => "video/3gpp",
                ".mov" => "video/quicktime",
                ".flv" => "video/x-flv",
                ".rm" => "application/vnd.rn-realmedia",  // For .rm files
                ".wmv" => "video/x-ms-wmv", // For .wmv files
                _ => "application/octet-stream"  // MIME type if unknown
            };


            // 4  - read the file as bytes 
            byte[] bytess = await System.IO.File.ReadAllBytesAsync(path);




            return new IMGDto { imagesbytes = bytess, MemoType = mimeType };

        }


        #endregion
        public async Task<userdatadto> GetUserDataAsync(string user_id)
        {

            var user = await _userRepositry.GetByIdAsync(user_id);
            if (user == null)
                throw new Exception("User Not Found ! ");
            userdatadto User = new userdatadto
            {
                FirstName=user.FirstName,
                LastName=user.LastName,
                Email=user.Email,
                NID=user.NationalID
            };
            return User;
        }

        public async Task<List<userdatadto>> GetAllUsersAsync()
        {
            var users = await _userRepositry.GetAlLlUsersAsync();
            

            List<userdatadto> Users = new List<userdatadto>();


            foreach(var user in users)
            {
                userdatadto userdatadto = new userdatadto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    NID = user.NationalID
                };

                Users.Add(userdatadto);
            }

            return Users;
           
        }

        public async Task UpdateUserAsync(UpdateUserDto updateduser)
        {
            var find_user = await _userRepositry.GetByIdAsync(updateduser.userid);
            if (find_user  == null)
                throw new Exception("User Not Found ! ");
            find_user.FirstName = updateduser.FirstName;
            find_user.LastName = updateduser.LastName;

           await _userRepositry.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userid)
        {
            var user = await _userRepositry.GetByIdAsync(userid);

            await _userRepositry.DeleteAsync(userid);

            await _userRepositry.SaveChangesAsync();
        }

        public async Task DeleteAllUsersAsync()
        {

            await _userRepositry.DeleteAllAsync();
            await _userRepositry.SaveChangesAsync();
        }

       
    }
}
