namespace MovieHub.Services.Interfaces
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;

    public interface IMovieService
    {
        ICollection<Movie> SearchMoviesByTitle(string Title);
        ICollection<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        int AddMovie(Movie movieDTO);
    }
}
