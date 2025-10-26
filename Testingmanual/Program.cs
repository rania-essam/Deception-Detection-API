using Microsoft.EntityFrameworkCore;
using TruthDetection.DAL.Data.DbHelper;
using TruthDetection.DAL.Data.Models;

namespace Testingmanual
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationUser user = new ApplicationUser
            {
                NationalID="12345678918765",
                Email="raniaessam@yahoo.com",
                PasswordHash="sjkwdw##",
                FirstName="Rn",
                LastName="Es",
                ProfilePictureURL="1.jbg"
            };

            // Configure DbContextOptions (in-memory database for testing)
            var options = new DbContextOptionsBuilder<TruthDetectionContext>()
                .UseInMemoryDatabase("TestDatabase")  // Use your preferred database provider here
                .Options;

            TruthDetectionContext _context = new TruthDetectionContext(options);
            _context.User.Add(user);
            _context.SaveChanges();

            var users = _context.User.ToList();

            foreach(var userr in users)
            {
                    Console.WriteLine(userr.Id);
                Console.WriteLine(userr.LastName);
                Console.WriteLine(userr.FirstName);
                Console.WriteLine(userr.Email);
                Console.WriteLine(userr.NationalID);

            }


        }
    }
}
