namespace MovieHub.Data
{
    using Import;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class SeedInitializer : DropCreateDatabaseIfModelChanges<MovieDbContext>
    {
        //TODO : Replace that path depending on where movies.json is on your PC
        //private const string filePath = "../../../MovieHub/import/movies.json";
        private const string filePath = @"D:\Github\MovieHub\MovieHub\Import\movies.json";

        protected override void Seed(MovieDbContext context)
        {
            ImportDirectors();
            ImportGenres();
            ImportProductions();
            ImportActors();
            ImportMovies();

            AddRoles();
            CreateAdminUser(context);

            base.Seed(context);
        }

        private void AddRoles()
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                if (!context.Roles.Any())
                {
                    CreateRole(context, "Administrator");
                    CreateRole(context, "User");
                }
            }
        }
        private void CreateAdminUser(MovieDbContext context)
        {
            ApplicationUser adminUser = new ApplicationUser()
            {
                UserName = "admin",
                Email = "admin@admin.com",
                FirstName = "System",
                LastName = "Administrator",
                Gender = GenderType.Male
            };

            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };

            var adminCreateResult = userManager.Create(adminUser, "admin");
            if (!adminCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", adminCreateResult.Errors));
            }

            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, "Administrator");
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateRole(MovieDbContext context, string roleName)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            IdentityResult result = roleManager.Create(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors));
            }
        }

        private void ImportActors()
        {
            ICollection<Actor> actors = JsonImport.ImportActors();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Actors.AddRange(actors);
                context.SaveChanges();
            }
        }

        //private static List<string> GetDistinctActorNames(string json)
        //{
        //    List<CsvActorsDTO> actorNamesCSV = JsonConvert.DeserializeObject<List<CsvActorsDTO>>(json);
        //    List<string> actorNames = new List<string>();

        //    foreach (var item in actorNamesCSV)
        //    {
        //        string[] actors = item.Actors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
        //        actorNames.AddRange(actors);
        //    }

        //    return actorNames.Distinct().ToList();
        //}

        private void ImportProductions()
        {
            //string json = File.ReadAllText(filePath);

            //List<Production> productions = JsonConvert.DeserializeObject<List<Production>>(json);

            //foreach (var prod in productions)
            //{
            //    if (!context.Productions.Any(p => p.Name == prod.Name))
            //    {
            //        context.Productions.Add(prod);
            //        context.SaveChanges();
            //    }
            //}

            ICollection<Production> productions = JsonImport.ImportProductions();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Productions.AddRange(productions);
                context.SaveChanges();
            }
        }

        private void ImportGenres()
        {
            //string json = File.ReadAllText(filePath);

            //List<string> genreNames = GetDistinctGenreNames(json);
            //List<Genre> genres = Mapper.Map<List<string>, List<Genre>>(genreNames);

            //context.Genres.AddRange(genres);
            //context.SaveChanges();

            ICollection<Genre> genres = JsonImport.ImportGenres();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }

        //private static List<string> GetDistinctGenreNames(string json)
        //{
        //    List<CsvGenresDTO> genresCSVs = JsonConvert.DeserializeObject<List<CsvGenresDTO>>(json);

        //    List<string> genresList = new List<string>();

        //    foreach (var g in genresCSVs)
        //    {
        //        string[] genres = g.GenresCSV.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        genresList.AddRange(genres);
        //    }

        //    genresList = genresList.Distinct().ToList();
        //    return genresList;
        //}

        //private static List<string> GetDistinctDirectorNames(string json)
        //{
        //    List<CsvDirectorsDTO> directorNamesCSV = JsonConvert.DeserializeObject<List<CsvDirectorsDTO>>(json);

        //    List<string> directorNames = new List<string>();
        //    foreach (var csv in directorNamesCSV)
        //    {
        //        string[] names = csv.Directors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //        directorNames.AddRange(names);
        //    }

        //    directorNames = directorNames.Distinct().Select(d => d.Trim()).ToList();

        //    return directorNames;
        //}

        private void ImportDirectors()
        {
            //string json = File.ReadAllText(filePath);

            //List<string> directorNames = GetDistinctDirectorNames(json);
            //List<Director> directors = Mapper.Map<List<string>, List<Director>>(directorNames);

            //context.Directors.AddRange(directors);
            //context.SaveChanges();

            ICollection<Director> directors = JsonImport.ImportDirectors();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Directors.AddRange(directors);
                context.SaveChanges();
            }
        }


        private void ImportMovies()
        {


            //string moviesJson = File.ReadAllText(filePath);

            //List<MovieDTO> movieDTOs = JsonConvert.DeserializeObject<List<MovieDTO>>(moviesJson,
            //    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            //foreach (var movieDTO in movieDTOs)
            //{
            //    List<Genre> genres = GetGenresByName(context, movieDTO.Genres);
            //    Director director = GetDirectorByName(context, movieDTO.DirectorName);
            //    List<Actor> actors = GetActorsByName(context, movieDTO.ActorNames);
            //    string[] languages = movieDTO.Language.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //    Production production = GetProductionByName(context, movieDTO.Production);

            //    Movie movie = new Movie()
            //    {
            //        Title = movieDTO.Title,
            //        Awards = movieDTO.Awards,
            //        Director = director,
            //        Genres = genres,
            //        Actors = actors,
            //        ImdbRating = movieDTO.imdbRating,
            //        Language = languages,
            //        Plot = movieDTO.Plot,
            //        PosterUrl = movieDTO.Poster,
            //        Year = movieDTO.Year,
            //        Runtime = movieDTO.Runtime,
            //        Production = production,
            //        Released = movieDTO.Released,
            //        Rated = movieDTO.Rated
            //    };

            //    context.Movies.Add(movie);
            //}

            //context.SaveChanges();

            using (MovieDbContext context = new MovieDbContext())
            {
                ICollection<Movie> movies = JsonImport.ImportMovies(context);

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }

        //private Production GetProductionByName(MovieDbContext context, string productionName)
        //{
        //    return context.Productions.FirstOrDefault(p => p.Name == productionName);
        //}

        //public static List<Actor> GetActorsByName(MovieDbContext context, string actorsCSV)
        //{
        //    string[] actorNames = actorsCSV
        //        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //        .Select(a => a.Trim())
        //        .ToArray();
        //    List<Actor> actorsList = new List<Actor>();

        //    foreach (var actorName in actorNames)
        //    {
        //        Actor actor = context.Actors.FirstOrDefault(a => a.Name == actorName);
        //        actorsList.Add(actor);
        //    }

        //    return actorsList;
        //}

        //public static Director GetDirectorByName(MovieDbContext context, string directorName)
        //{
        //    return context.Directors
        //        .FirstOrDefault(d => d.Name == directorName);
        //}

        //public static List<Genre> GetGenresByName(MovieDbContext context, string genresCSV)
        //{
        //    string[] genresNames = genresCSV.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        //    List<Genre> genresList = new List<Genre>();

        //    foreach (var genreName in genresNames)
        //    {
        //        Genre genre = context.Genres.FirstOrDefault(g => g.Name == genreName);
        //        genresList.Add(genre);
        //    }

        //    return genresList;
        //}
    }
}

