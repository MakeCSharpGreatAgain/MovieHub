namespace MovieHub.Utils
{
    using Models;
    using Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.Movie;

    public static class Utils
    {
        public static Production GetProduction(CreateViewModel movieViewModel)
        {
            IProductionService productionService = ServiceLocator.Instance.GetService<IProductionService>();

            Production production = productionService.GetProductionByName(movieViewModel.Production);

            if (production == null)
            {
                production = productionService.InsertProduction(movieViewModel.Production);
            }

            return production;
        }

        public static Director GetDirector(CreateViewModel movieViewModel)
        {
            IDirectorService directorService = ServiceLocator.Instance.GetService<IDirectorService>();

            Director director = directorService.GetDirectorByName(movieViewModel.DirectorName);

            if (director == null)
            {
                director = directorService.InsertDirector(movieViewModel.DirectorName);
            }

            return director;
        }

        public static ICollection<Genre> GetGenres(string genreNames)
        {
            IGenreService genreService = ServiceLocator.Instance.GetService<IGenreService>();

            ICollection<Genre> genres = new List<Genre>();
            string[] genresArray = genreNames.
                Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(g => g.Trim())
                .ToArray();

            foreach (var name in genresArray)
            {
                if (!genres.Any(a => a.Name.ToLower() == name.ToLower()))
                {
                    Genre genre = genreService.GetGenreByName(name);
                    if (genre == null)
                    {
                        genre = genreService.InsertGenre(name);
                    }

                    genres.Add(genre);
                }
            }

            return genres;
        }

        public static ICollection<Actor> GetActors(string actorNames)
        {
            IActorService actorService = ServiceLocator.Instance.GetService<IActorService>();

            ICollection<Actor> actors = new List<Actor>();
            string[] actorsArray = actorNames
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToArray();

            foreach (var name in actorsArray)
            {
                if (!actors.Any(a => a.Name.ToLower() == name.ToLower()))
                {
                    Actor actor = actorService.GetActorByName(name);
                    if (actor == null)
                    {
                        actor = actorService.InsertActor(name);
                    }

                    actors.Add(actor);
                }
            }

            return actors;
        }
    }
}