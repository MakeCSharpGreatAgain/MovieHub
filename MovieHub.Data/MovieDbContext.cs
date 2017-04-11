using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Models;

namespace MovieHub.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<MovieDbContext>(new SeedInitializer());

        }


        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }
        public virtual DbSet<Movie> Movies { get; set; }


        public void Seed(MovieDbContext context)
        {
            ImportMovies(context);

        }

        private void ImportMovies(MovieDbContext context)
        {
            //TODO : Replace that path depending on where movies.json is on your PC
            string filePath = @"D:\Github\MovieHub\MovieHub\import\movies.json";
            string moviesJson = File.ReadAllText(filePath);


            Movie movie = new Movie()
            {
                Title = "A movie title"
            };


            context.Movies.Add(movie);
            context.SaveChanges();

        }
    }

    public class SeedInitializer : DropCreateDatabaseAlways<MovieDbContext>
    {
        protected override void Seed(MovieDbContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

}
