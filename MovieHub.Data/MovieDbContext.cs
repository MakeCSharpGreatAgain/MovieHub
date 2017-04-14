using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Models;

namespace MovieHub.Data
{
    using Configurations;

    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext()
            : base("MovieHubContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<MovieDbContext>(new SeedInitializer());
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Production> Productions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MovieConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }
    }
}
