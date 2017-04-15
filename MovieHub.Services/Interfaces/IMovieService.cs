namespace MovieHub.Services.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IMovieService
    {
        ICollection<Movie> GetAllMovies();
        Movie GetMovieById(int id); 
    }
}
