using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Models;

namespace MovieHub.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class MovieContext : IdentityDbContext<ApplicationUser>
    {
        public MovieContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }



        public static MovieContext Create()
        {
            return new MovieContext();
        }
    }
}