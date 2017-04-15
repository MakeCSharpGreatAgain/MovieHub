namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Collections.Generic;
    using Data;
    using System.Linq;

    public class DirectorService : IDirectorService
    {
        public void InsertDirectors(ICollection<Director> directors)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Directors.AddRange(directors);
            }
        }

        public Director InsertDirector(string name)
        {
            Director director = new Director()
            {
                Name = name
            };

            using (MovieDbContext context = new MovieDbContext())
            {
                context.Directors.Add(director);
                context.SaveChanges();
            }

            return director;
        }

        //public bool IsDirectorExisting(string name)
        //{
        //    using (MovieDbContext context = new MovieDbContext())
        //    {
        //        return context.Directors.Any(d => d.Name.ToLower() == name.ToLower());
        //    }
        //}

        public Director GetDirectorByName(string name)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                return context.Directors.FirstOrDefault(d => d.Name.ToLower() == name.ToLower());
            }
        }
    }
}
