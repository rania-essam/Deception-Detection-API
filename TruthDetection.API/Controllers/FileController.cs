using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing.Constraints;
using TruthDetection.BLL.Dtos;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        private readonly IUserService userService;
        private readonly IUserRepositry userRepositry;
        public FileController(IUserService userService , IUserRepositry userRepositry)
        {
            this.userService = userService;
            this.userRepositry = userRepositry;
        }

        #region Image
        // Insert / Update / Delete / Read Image 

        [HttpPost("AddFile")]    // Insert , Update

       public async Task<IActionResult> AddFile([FromForm] AddFileDto image)
       {

            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
          
            string path = await userService.UploadImageAsync(image );

            if( path == "User Not Found !")
            {
                return BadRequest("User Not Found");
               
            }
            if(path == "File is required !")
            {
                return BadRequest("File is required ");
            }

            return Ok($"file uploaded successfully !");
       }

        [HttpDelete("DeleteFile")] // Delete

        public async Task<IActionResult> DeleteFile(string  Id, string filename)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            string folderName = "Images";
            DeleteFileDto deleteFileDto = new DeleteFileDto
            {
                FolderName = folderName,
                FileName = filename,
                ID = Id

            };
            string result = await userService.DeleteFile(deleteFileDto);
            if (result == "UserNotFound")
            {
                return BadRequest("User Not Found");
            }

            return Ok("File Deleted Successfully !");


        }

        [HttpPost("ShowFile")]        // Read image 
        public async Task<IActionResult> ShowFile ([FromBody] ShowFileDto showIMG)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            IMGDto data = await userService.ShowFileAsync(showIMG);
            if (data == null || data.imagesbytes == null || data.imagesbytes.Length == 0)
            {
                return NotFound("Image not found or invalid data.");
            }
            return File(data.imagesbytes, data.MemoType);


        }

        [HttpPost("DownloadFile")]
        public async Task<IActionResult> DownloadFile (DownloadImageDto downloadimage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            IMGDto data = await userService.DownloadImageAsync(downloadimage);
            if (data == null || data.imagesbytes == null || data.imagesbytes.Length == 0)
            {
                return NotFound("Image not found or invalid data.");
            }
            var imagename = Path.GetFileName(downloadimage.ImageName);

            return File(data.imagesbytes, data.MemoType ,imagename );

        }
        #endregion

        // ADD / Update / delete  / show  [ video ]


        #region Video

        // return size of the file

        [HttpPost("FileSize")]

     //   [RequestSizeLimit(512*1024*1024)]
        public  IActionResult FileSize(IFormFile file)
        {
            var filesizeInMBs = file.Length / (1024.0 * 1024.0);

            return Ok(filesizeInMBs);
        }



        [HttpDelete("DeleteVideo")] // Delete

        public async Task<IActionResult> DeleteVideo(string UserID ,[FromBody]DeleteVideoDto deleteVideoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            var user =await userRepositry.GetByIdAsync(UserID);
            if (user == null)
            {
                return BadRequest("User Not Found!");
            }

            string result = await userService.DeleteVideoAsync(deleteVideoDto);
            
            if(result== "Video Not Found !")
            {
                return BadRequest("User Not Found");
            }

            return Ok("Video Deleted Successfully !");

        }

        [HttpPost("AddVideo")]
        public async Task<IActionResult> AddVideo([FromForm]AddFileDto fileDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }

            string path = await userService.UploadVideoAsync(fileDto);

            if (path == "User Not Found !")
            {
                return BadRequest("User Not Found");

            }
            if (path == "File is required !")
            {
                return BadRequest("File is required ");
            }

            return Ok($"file uploaded successfully !");
        }


        [HttpPost("Show Video")]

        public async Task<IActionResult> ShowVideo([FromBody]ShowVideoDto videoDto) // userid , videoid 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            IMGDto data = await userService.ShowVideoAsync(videoDto);
            if (data == null || data.imagesbytes == null || data.imagesbytes.Length == 0)
            {
                return NotFound("Video not found or invalid data.");
            }
            return File(data.imagesbytes, data.MemoType , enableRangeProcessing: true);
        }

        [HttpPost("DownloadVideo")]
        public async Task<IActionResult> DownloadVideo([FromBody] ShowVideoDto videoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Something Went Wrong !");
            }
            IMGDto data = await userService.DownloadVideoAsync(videoDto);
            if (data == null || data.imagesbytes == null || data.imagesbytes.Length == 0)
            {
                return NotFound("Image not found or invalid data.");
            }
            var imagename = Path.GetFileName(videoDto.VideoName);

            return File(data.imagesbytes, data.MemoType, imagename);

        }


        #endregion

    }
}
