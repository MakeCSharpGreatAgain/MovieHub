namespace MovieHub.Data.Import
{
    using AutoMapper;
    using Models;
    using Models.DTOs;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Utils;

    public static class JsonImport
    {
        private const string FilePath = @"D:\Github\MovieHub\MovieHub\Import\movies.json";

        public static ICollection<Movie> ImportMovies(MovieDbContext context)
        {
            string json = File.ReadAllText(FilePath);
            ICollection<MovieDTO> movieDTOs = JsonConvert.DeserializeObject<ICollection<MovieDTO>>(json, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            ICollection<Movie> movies = new List<Movie>();

            foreach (var movieDTO in movieDTOs)
            {
                ICollection<Genre> genres =
                    ImportHelper.GetGenresByName(context, movieDTO.Genres);
                ICollection<Actor> actors =
                    ImportHelper.GetActorsByName(context, movieDTO.ActorNames);
                Director director =
                    ImportHelper.GetDirectorByName(context, movieDTO.DirectorName);
                Production production =
                    ImportHelper.GetProductionByName(context, movieDTO.Production);
                //string[] languages = movieDTO.Language.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int runtime = int.Parse(movieDTO.Runtime.Split(' ').First());

                Movie movie = new Movie()
                {
                    Title = movieDTO.Title,
                    Awards = movieDTO.Awards,
                    Director = director,
                    Genres = genres,
                    Actors = actors,
                    ImdbRating = movieDTO.ImdbRating,
                    Languages = movieDTO.Languages,
                    Plot = movieDTO.Plot,
                    PosterUrl = movieDTO.Poster,
                    //Year = movieDTO.Year,
                    Runtime = runtime,
                    Production = production,
                    Released = movieDTO.Released,
                    Rated = movieDTO.Rated
                };

                movies.Add(movie);
            }

            return movies;
        }

        public static ICollection<Actor> ImportActors()
        {
            string json = File.ReadAllText(FilePath);
            IEnumerable<CsvActorsDTO> actorNamesCSV = JsonConvert.DeserializeObject<IEnumerable<CsvActorsDTO>>(json);

            ICollection<string> actorNames =
                ImportHelper.GetDistinctActorNames(actorNamesCSV);

            ICollection<Actor> actors =
                Mapper.Map<ICollection<string>, ICollection<Actor>>(actorNames);

            return actors;
        }

        public static ICollection<Director> ImportDirectors()
        {
            string json = File.ReadAllText(FilePath);
            IEnumerable<CsvDirectorsDTO> directorNamesCSV = JsonConvert.DeserializeObject<IEnumerable<CsvDirectorsDTO>>(json);

            ICollection<string> directorNames =
                ImportHelper.GetDistinctDirectorNames(directorNamesCSV);

            ICollection<Director> directors =
                Mapper.Map<ICollection<string>, ICollection<Director>>(directorNames);

            return directors;
        }

        public static ICollection<Genre> ImportGenres()
        {
            string json = File.ReadAllText(FilePath);
            IEnumerable<CsvGenresDTO> genreNamesCSV = JsonConvert.DeserializeObject<IEnumerable<CsvGenresDTO>>(json);

            ICollection<string> genreNames =
                ImportHelper.GetDistinctGenreNames(genreNamesCSV);

            ICollection<Genre> genres =
                Mapper.Map<ICollection<string>, ICollection<Genre>>(genreNames);

            return genres;
        }

        public static ICollection<Production> ImportProductions()
        {
            string json = File.ReadAllText(FilePath);
            ICollection<Production> productions = ImportHelper.GetDistinctProductions(JsonConvert.DeserializeObject<ICollection<Production>>(json));

            return productions;
        }
    }
}
