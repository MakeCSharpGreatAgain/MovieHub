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


        private void ImportProductions()
        {
            ICollection<Production> productions = JsonImport.ImportProductions();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Productions.AddRange(productions);
                context.SaveChanges();
            }
        }

        private void ImportGenres()
        {
            ICollection<Genre> genres = JsonImport.ImportGenres();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }
        }

        private void ImportDirectors()
        {
            ICollection<Director> directors = JsonImport.ImportDirectors();
            using (MovieDbContext context = new MovieDbContext())
            {
                context.Directors.AddRange(directors);
                context.SaveChanges();
            }
        }

        private void ImportMovies()
        {

            using (MovieDbContext context = new MovieDbContext())
            {
                ICollection<Movie> movies = JsonImport.ImportMovies(context);

                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }
    }
}

