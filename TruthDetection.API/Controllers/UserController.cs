using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruthDetection.BLL.Dtos;
using TruthDetection.BLL.Managers.Interfaces;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Interfaces;

namespace TruthDetection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IVideoService _videoservice;

        public UserController(IUserService _userService, IVideoService _videoservice)
        {
            this._userService = _userService;
            this._videoservice = _videoservice;
        }
        #region Notes

        //[HttpGet("{Name:alpha} / {id:int}")]

        ///*
        // There are 2 options :
        // 1 - from query string ( https://localhost:7145/api/User?id=1&Name=rania ) 
        // 2 - from path  ( https://localhost:7145/api/User/{Name} / {id} )
        // */
        //public IActionResult fun(int id , string Name) 
        //{
        //    return Ok();
        //}

        //[HttpPost]

        //public IActionResult f(Result result, int id)
        //{
        //    return Ok();
        //}


        // Get User ByID

        #endregion

        [HttpGet("{ID}")]  // Get UserBYID

        public async Task<IActionResult> GetUserAsync(string ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.GetUserDataAsync(ID);
            return Ok(user);
        }

        [HttpGet("GetUsers")] // get all users <------- Adminnnn

        public async Task<IActionResult> GetAllUsersAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }




        [HttpPut("UpdateUser")] //Update User
        public async Task<IActionResult> UpadteUserAsync(UpdateUserDto updateUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateUserAsync(updateUser);
            return Ok();
        }



        [HttpDelete("User")] // delete user ==> delete video ==> delete result ==> delete result details
        public async Task<IActionResult> DeleteUserAsync(string ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             await _userService.DeleteUserAsync(ID);
            return Ok("User Deleted Successfully !");
        }

        [HttpDelete("Users")] // delete user ==> delete video ==> delete result ==> delete result details
        public async Task<IActionResult> DeleteAllUsersAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userService.DeleteAllUsersAsync();
            return Ok();
        }

        [HttpPost("AddResult")]

        public async Task<IActionResult> AddResultAsync([FromBody]int video_id, [FromBody]bool detection_result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _videoservice.AddResultAsync(video_id, detection_result);

            return Ok("Result Added ");
        }






    }
}
