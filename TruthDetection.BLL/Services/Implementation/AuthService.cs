
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TruthDetection.BLL.Dtos;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.Models;

namespace TruthDetection.BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configure;
        public AuthService(UserManager<ApplicationUser> _userManager , IConfiguration congfigure , RoleManager<IdentityRole> _roleManager)
        {
            this._userManager = _userManager;
            this.configure = congfigure;
            this._roleManager = _roleManager;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model) 
        {
            var user = await _userManager.FindByIdAsync(model.userid);

            // Role Doesn’t Exist or User Doesn’t Exist 
            if (user is null || !await _roleManager.RoleExistsAsync(model.Role)) 
                return "Invalid UserID or Role";

            // if the user assigned already to the role
            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User Already Assigned to This Role";

            var result =  await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something Went Wrong";       
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model) // For login 
        {

            AuthModel authmodel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if( user is null  || !await _userManager.CheckPasswordAsync(user , model.Password))
            {
                authmodel.Message = "Email Or Password is Not Correct ";
                return authmodel;
            }

            // create token 
            var jwtSecurityToken = await CreateJwtToken(user);
            var roleslist = await _userManager.GetRolesAsync(user);

            authmodel.UserName = user.UserName;
            authmodel.IsAuthenticated = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authmodel.Email = user.Email;
            authmodel.ExpiresOn = jwtSecurityToken.ValidTo;
            authmodel.Roles = roleslist.ToList();

            return authmodel;
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto UserFromRequest)
        {
            if (await _userManager.FindByEmailAsync(UserFromRequest.Email) != null)
                return new AuthModel { Message =" Email is Already Registered ! "}; // IsAuthenticated == false ( ByDefault )
            if (await _userManager.FindByEmailAsync(UserFromRequest.UserName) != null)
                return new AuthModel { Message = " UserName is Already Registered ! " }; // IsAuthenticated == false ( ByDefault )

            if (UserFromRequest.Password != UserFromRequest.ConfirmPassword)
                return new AuthModel { Message = "Passwors Doesn’t match " };
            ApplicationUser user = new ApplicationUser
            {
                FirstName = UserFromRequest.first_name,
                LastName = UserFromRequest.last_name,
                Email = UserFromRequest.Email,
                NationalID = UserFromRequest.Nid,
                UserName = UserFromRequest.UserName
            };


            IdentityResult result = await _userManager.CreateAsync(user, UserFromRequest.Password);

            // if creation failed return errors to user
            if (!result.Succeeded)
            {
                string Errors = String.Empty;
                foreach(var error in result.Errors)
                {
                    Errors += $"{error.Description} ,";
                }
                return new AuthModel { Message = Errors };

            }
            //creation successed 

            // add role

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel  {
                       IsAuthenticated=true,
                       UserName=user.UserName,
                       Token= new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                       Roles=new List<string> {"User"},
                       ExpiresOn = jwtSecurityToken.ValidTo

            };
            


        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id) // or national id ?
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configure["JWT:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
             audience: configure["JWT:AudienceIP"],// angular local host is 4200
             issuer: configure["JWT:IssuerIP"],
             expires: DateTime.Now.AddDays(double.Parse(configure["JWT:Duration"])),
             claims: claims,
             signingCredentials: signingCredentials
             );

            return jwtSecurityToken;
        }
    }
}
