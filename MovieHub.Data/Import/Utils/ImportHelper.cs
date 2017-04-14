namespace MovieHub.Data.Import.Utils
{
    using Models;
    using Models.DTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ImportHelper
    {
        public static Production GetProductionByName(MovieDbContext context, string productionName)
        {
            return context.Productions
                .FirstOrDefault(p => p.Name == productionName);
        }

        public static Director GetDirectorByName(MovieDbContext context, string directorName)
        {
            return context.Directors
            .FirstOrDefault(d => d.Name == directorName);
        }

        public static ICollection<Actor> GetActorsByName(MovieDbContext context, string actorsCSV)
        {
            string[] actorNames = actorsCSV
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Trim())
                .ToArray();

            ICollection<Actor> actors = new List<Actor>();

            foreach (var actorName in actorNames)
            {
                Actor actor = context.Actors.FirstOrDefault(a => a.Name == actorName);
                actors.Add(actor);
            }

            return actors;
        }

        public static ICollection<Genre> GetGenresByName(MovieDbContext context, string genresCSV)
        {
            string[] genresNames = genresCSV.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            ICollection<Genre> genres = new List<Genre>();

            foreach (var genreName in genresNames)
            {
                Genre genre = context.Genres.FirstOrDefault(g => g.Name == genreName);
                genres.Add(genre);
            }

            return genres;
        }

        public static ICollection<string> GetDistinctActorNames(IEnumerable<CsvActorsDTO> actorNamesCSV)
        {
            ICollection<string> actorNames = new List<string>();

            foreach (var csv in actorNamesCSV)
            {
                string[] names = csv.Actors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var name in names)
                {
                    if (!actorNames.Contains(name.Trim()))
                    {
                        actorNames.Add(name.Trim());
                    }
                }
            }

            return actorNames;
        }

        public static ICollection<Production> GetDistinctProductions(ICollection<Production> productions)
        {
            ICollection<Production> distinctProductions = new List<Production>();

            foreach (var prod in productions)
            {
                if (!distinctProductions.Any(p => p.Name == prod.Name))
                {
                    distinctProductions.Add(prod);
                }
            }

            return distinctProductions;
        }
        public static ICollection<string> GetDistinctDirectorNames(IEnumerable<CsvDirectorsDTO> directorNamesCSV)
        {
            ICollection<string> directorNames = new List<string>();

            foreach (var csv in directorNamesCSV)
            {
                string[] names = csv.Directors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var name in names)
                {
                    if (!directorNames.Contains(name.Trim()))
                    {
                        directorNames.Add(name.Trim());
                    }
                }
            }

            return directorNames;
        }

        public static ICollection<string> GetDistinctGenreNames(IEnumerable<CsvGenresDTO> genreNamesCSV)
        {
            ICollection<string> genreNames = new List<string>();

            foreach (var csv in genreNamesCSV)
            {
                string[] names = csv.GenresCSV.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var name in names)
                {
                    if (!genreNames.Contains(name.Trim()))
                    {
                        genreNames.Add(name.Trim());
                    }
                }
            }

            return genreNames;
        }

    }
}
