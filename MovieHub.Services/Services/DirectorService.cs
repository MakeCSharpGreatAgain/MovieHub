namespace MovieHub.Services.Services
{
    using Interfaces;
    using System;
    using Models;
    using System.Collections.Generic;
    using Data;

    public class DirectorService : IDirectorService
    {
        public void InsertDirectors(ICollection<Director> directors)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Directors.AddRange(directors);
            }
        }
    }
}
