
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruthDetection.BLL.Dtos;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        private readonly IAuthService _authService; // from BLL services 

        public AuthController(IAuthService _authService)
        {
            this._authService = _authService;
        }

        [HttpPost("register")] // api/controller/register

        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto UserFromRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(UserFromRequest);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            return Ok(result);

           // return Ok(new { }); ==> return anonymous object with the values you want (return not all result ) ( based on BL )


        }


        [HttpPost("login")] // api/controller/login

        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);


            return Ok(result);

            // return Ok(new { }); ==> return anonymous object with the values you want (return not all result ) ( based on BL )


        }


        [Authorize(Roles ="Admin")]
        [HttpPost("Addrole")] // api/controller/login

        public async Task<IActionResult> AddRoleAsync([FromBody]AddRoleModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result)) // string contain error message 
                return BadRequest(result);



            return Ok(model);

            // return Ok(new { }); ==> return anonymous object with the values you want (return not all result ) ( based on BL )


        }





    }
}
