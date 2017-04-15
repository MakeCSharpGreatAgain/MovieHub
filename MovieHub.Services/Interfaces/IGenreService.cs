using MovieHub.Models;

namespace MovieHub.Services.Interfaces
{
    public interface IGenreService
    {
        Genre GetGenreByName(string name);

        Genre InsertGenre(string name);
    }
}
