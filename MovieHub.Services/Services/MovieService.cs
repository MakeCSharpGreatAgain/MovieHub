namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Collections.Generic;
    using Data;
    using System.Linq;
    using System.Data.Entity;

    public class MovieService : IMovieService
    {
        public ICollection<Movie> GetAllMovies()
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Director)
                    .Include(m => m.Production)
                    .Include(m => m.Actors)
                    .ToList();
            }
        }

        public Movie GetMovieById(int id)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Director)
                    .Include(m => m.Production)
                    .Include(m => m.Actors)
                    .FirstOrDefault(m => m.Id == id);
            }
        }
    }
}
