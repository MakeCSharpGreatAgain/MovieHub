namespace MovieHub.Data.Configurations
{
    using Models;
    using System;
    using System.Data.Entity.ModelConfiguration;

    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            //Primary Key:
            this.HasKey(m => m.Id);

            //Title:
            this.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            ////Year:
            //this.Property(m => m.Year)
            //    .IsRequired();

            //Rated:
            this.Property(m => m.Rated)
                .IsRequired();

            //Runtime
            this.Property(m => m.Runtime)
                .IsRequired();

            //Plot:
            this.Property(m => m.Plot)
                .IsRequired();

            //ImdbRating:
            this.Property(m => m.ImdbRating)
                .IsRequired();

            //One-to-Many (Director-Movies)
            this.HasOptional(m => m.Director)
                .WithMany(d => d.DirectedMovies)
                .HasForeignKey(m => m.DirectorId);

            //One-to-Many (Production-Movies)
            this.HasOptional(m => m.Production)
                .WithMany(p => p.Movies)
                .HasForeignKey(m => m.ProductionId);

            //Many-to-Many (Genres-Movies)
            this.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .Map(cfg =>
                {
                    cfg.MapLeftKey("MovieId");
                    cfg.MapRightKey("GenreId");
                    cfg.ToTable("MovieGenres");
                });

            //Many-to-Many (Actors-Movies)
            this.HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .Map(cfg =>
                {
                    cfg.MapLeftKey("MovieId");
                    cfg.MapRightKey("ActorId");
                    cfg.ToTable("MovieActors");
                });
        }
    }
}
