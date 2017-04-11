using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Models;
using Newtonsoft.Json;

namespace MovieHub.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class MovieContext : IdentityDbContext<ApplicationUser>
    {
        public MovieContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<MovieContext>(new SeedInitializer());

        }

        public virtual DbSet<Movie> Movies { get; set; }

        public static MovieContext Create()
        {
            return new MovieContext();
        }

        public void Seed(MovieContext context)
        {
            ImportMovies(context);

        }

        private void ImportMovies(MovieContext context)
        {
            //TODO : Replace that path depending on where movies.json is on your PC
            string filePath = @"D:\Github\MovieHub\MovieHub\import\movies.json";
            string moviesJson = File.ReadAllText(filePath);
            
            
            Movie movie = new Movie()
            {
                Title = "Movie Title"
            };


            context.Movies.Add(movie);
            context.SaveChanges();

        }
    }

    public class SeedInitializer : DropCreateDatabaseAlways<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }
}