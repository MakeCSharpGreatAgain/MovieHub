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
using MovieHub.Data.Configurations;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MovieConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static MovieDbContext Create()
        {
            return new MovieDbContext();
        }
    }
}
