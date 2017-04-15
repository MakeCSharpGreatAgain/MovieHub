namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using Data;
    using System.Linq;

    public class GenreService : IGenreService
    {
        public Genre GetGenreByName(string name)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Genres.FirstOrDefault(g => g.Name.ToLower() == name.ToLower());
            }
        }

        public Genre InsertGenre(string name)
        {
            Genre genre = new Genre()
            {
                Name = name
            };

            using (MovieDbContext context = new MovieDbContext())
            {
                context.Genres.Add(genre);
                context.SaveChanges();
            }

            return genre;
        }
    }
}
