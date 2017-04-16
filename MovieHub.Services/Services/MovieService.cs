namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Collections.Generic;
    using Data;
    using System.Linq;
    using System.Data.Entity;
    using Models.DTOs;

    public class MovieService : IMovieService
    {
        public int AddMovie(Movie movie)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                foreach (var actor in movie.Actors)
                {
                    context.Entry(actor).State = EntityState.Unchanged;
                }

                foreach (var genre in movie.Genres)
                {
                    context.Entry(genre).State = EntityState.Unchanged;
                }

                if (movie.Director != null)
                {
                    context.Entry(movie.Director).State = EntityState.Unchanged;
                }

                if (movie.Production != null)
                {
                    context.Entry(movie.Production).State = EntityState.Unchanged;
                }

                context.Movies.Add(movie);
                context.SaveChanges();

                return movie.Id;
            }
        }

        public ICollection<Movie> GetAllMovies()
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Movies
                    .Include(m => m.Genres)
                    .Include(m => m.Director)
                    .Include(m => m.Production)
                    .Include(m => m.Actors)
                    .Include(m => m.Reviews.Select(r => r.Author))
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
                    .Include(m => m.Reviews.Select(r => r.Author))
                    .FirstOrDefault(m => m.Id == id);
            }
        }

        public ICollection<Movie> SearchMoviesByTitle(string title)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Movies
                    .Where(m => m.Title.Contains(title) || title == null)
                    .ToList();
            }
        }
    }
}
