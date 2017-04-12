using System.Data.Entity;
using System.IO;
using Microsoft.AspNet.Identity.EntityFramework;
using MovieHub.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using MovieHub.Models.DTOs;
using System.Linq;
using AutoMapper;

namespace MovieHub.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


    public class MovieDbContext : IdentityDbContext<ApplicationUser>
    {
        public MovieDbContext()
            : base("MovieHubContext", throwIfV1Schema: false)
        {
            Database.SetInitializer<MovieDbContext>(new SeedInitializer());
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Production> Productions { get; set; }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }

        public void Seed(MovieDbContext context)
        {
            ImportDirectors(context);
            ImportGenres(context);
            ImportProductions(context);
            ImportActors(context);
            ImportMovies(context);
        }

        private void ImportActors(MovieDbContext context)
        {
            string filePath = @"E:\Softuni\Entity Framework\Teamwork\MovieHub\MovieHub\import\movies.json";
            string json = File.ReadAllText(filePath);

            List<string> actorNames = GetDistinctActorNames(json);
            List<Actor> actors = Mapper.Map<List<string>, List<Actor>>(actorNames);

            context.Actors.AddRange(actors);
            context.SaveChanges();
        }

        private static List<string> GetDistinctActorNames(string json)
        {
            List<CsvActorsDTO> actorNamesCSV = JsonConvert.DeserializeObject<List<CsvActorsDTO>>(json);
            List<string> actorNames = new List<string>();

            foreach (var item in actorNamesCSV)
            {
                string[] actors = item.Actors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                actorNames.AddRange(actors);
            }

            return actorNames.Distinct().ToList();
        }

        private void ImportProductions(MovieDbContext context)
        {
            string filePath = @"E:\Softuni\Entity Framework\Teamwork\MovieHub\MovieHub\import\movies.json";
            string json = File.ReadAllText(filePath);

            List<Production> productions = JsonConvert.DeserializeObject<List<Production>>(json);

            foreach (var prod in productions)
            {
                if (!context.Productions.Any(p => p.Name == prod.Name))
                {
                    context.Productions.Add(prod);
                    context.SaveChanges();
                }
            }
        }

        private void ImportGenres(MovieDbContext context)
        {
            string filePath = @"E:\Softuni\Entity Framework\Teamwork\MovieHub\MovieHub\import\movies.json";
            string json = File.ReadAllText(filePath);

            List<string> genreNames = GetDistinctGenreNames(json);
            List<Genre> genres = Mapper.Map<List<string>, List<Genre>>(genreNames);

            context.Genres.AddRange(genres);
            context.SaveChanges();
        }

        private static List<string> GetDistinctGenreNames(string json)
        {
            List<CsvGenresDTO> genresCSVs = JsonConvert.DeserializeObject<List<CsvGenresDTO>>(json);

            List<string> genresList = new List<string>();

            foreach (var g in genresCSVs)
            {
                string[] genres = g.GenresCSV.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                genresList.AddRange(genres);
            }

            genresList = genresList.Distinct().ToList();
            return genresList;
        }

        private static List<string> GEtDistinctDirectorNames(string json)
        {
            List<CsvDirectorsDTO> directorNamesCSV = JsonConvert.DeserializeObject<List<CsvDirectorsDTO>>(json);

            List<string> directorNames = new List<string>();
            foreach (var csv in directorNamesCSV)
            {
                string[] names = csv.Directors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                directorNames.AddRange(names);
            }

            directorNames = directorNames.Distinct().Select(d => d.Trim()).ToList();

            return directorNames;
        }

        private void ImportDirectors(MovieDbContext context)
        {
            string filePath = @"E:\Softuni\Entity Framework\Teamwork\MovieHub\MovieHub\import\movies.json";
            string json = File.ReadAllText(filePath);

            List<string> directorNames = GEtDistinctDirectorNames(json);
            List<Director> directors = Mapper.Map<List<string>, List<Director>>(directorNames);

            context.Directors.AddRange(directors);
            context.SaveChanges();
        }

        //TODO: Seed database with movies
        private void ImportMovies(MovieDbContext context)
        {
            //TODO : Replace that path depending on where movies.json is on your PC
            string filePath = @"E:\Softuni\Entity Framework\Teamwork\MovieHub\MovieHub\import\movies.json";
            string moviesJson = File.ReadAllText(filePath);

            //List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(moviesJson);

            //context.Movies.AddRange(movies);
            //context.SaveChanges();

        }
    }

    public class SeedInitializer : DropCreateDatabaseAlways<MovieDbContext>
    {
        protected override void Seed(MovieDbContext context)
        {
            context.Seed(context);
            base.Seed(context);
        }
    }

}
