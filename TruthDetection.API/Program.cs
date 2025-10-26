
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TruthDetection.BLL.Services.Implementation;
using TruthDetection.BLL.Services.Interfaces;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;
using TruthDetection.DAL.Repositries.Implementation;
using TruthDetection.DAL.Repositries.Interfaces;
using static System.Net.WebRequestMethods;

namespace TruthDetection.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region registerDbContext 
            // Register DbContext in IOC Container
            builder.Services.AddDbContext<TruthDetectionContext> ( 
                options => { options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));} 
                );

            #endregion


            #region Register Identity 
            // add identity services


            // Order matters because first Tuser , second TRole
            //after registring an identity you can use any services or datastores  related to user identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TruthDetectionContext>();


            #endregion


            #region AddAuthetication to middleware as middle ware uses cookies by default

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // tell him to look for "Bearer" in the request
                    // action have attributr [Autorize] and you not authorized ==> default is to return Unautorized 
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // IF Token not valid return " UnAutorized "
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }

                ).AddJwtBearer(
                options => // verify that the token is valid 
                {
                    options.SaveToken = true; // to ensure that the token is not expired 

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true, // TO ensure that the issuer is me
                        ValidIssuer = builder.Configuration["JWT:IssuerIP"], // if the issuer is wrong sets it to the right value
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:AudienceIP"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
                        ,
                        ValidateLifetime = true
                    };
                }
             );
            #endregion


            #region DI for repos
            builder.Services.AddScoped<IUserRepositry, UserRepository>();

            builder.Services.AddScoped<IVideoRepositry, VideoRepositry>();
            #endregion


            #region DI for services ( Managers )
            builder.Services.AddScoped<IAuthService,AuthService>();

    

            builder.Services.AddScoped<IUserService, UserService>();

            #endregion


            // configure Kestral (server ) to receive a body of big size ( globally ) to all endpoints

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 512 * 1024 * 1024;
            }
            );


            builder.Services.Configure<FormOptions>(  // allow body of bigsize locally ==> you should write  [RequestSizeLimit(512*1024*1024)] before the EndPoint
                options =>
                {
                    options.MultipartBodyLengthLimit = 512 * 1024 * 1024;
                }
                );
              
          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Enable serving static files from wwwroot
             app.UseStaticFiles(); 


             app.UseHttpsRedirection();

        //    app.UseAuthentication();  ==> exists by default ( searches about cookie until you tell it that you look for jwt )

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
