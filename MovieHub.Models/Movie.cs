using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Movie
    {
        private ICollection<Genre> genres;
        private ICollection<Actor> actors;

        public Movie()
        {
            this.genres = new HashSet<Genre>();
            this.actors = new HashSet<Actor>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        [MinLength(1, ErrorMessage = "{0} must be at least {1} symbols!")]
        public string Title { get; set; }

        public int Year { get; set; }

        public string Rated { get; set; }

        public DateTime? Released { get; set; }

        public string Runtime { get; set; }

        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }

        public string Plot { get; set; }

        public string[] Language { get; set; } // There could be more than 1.

        public string Awards { get; set; } // Simple string E.g. Nominated for 2 Oscars. Another 50 wins & 96 nominations.

        public string PosterUrl { get; set; } // This will be a link. Still no idea how to implement this!?

        public double ImdbRating { get; set; }

        public int? ProductionId { get; set; }
        public virtual Production Production { get; set; }

        public virtual ICollection<Genre> Genres
        {
            get
            {
                return genres;
            }

            set
            {
                genres = value;
            }
        }

        public virtual ICollection<Actor> Actors
        {
            get
            {
                return actors;
            }

            set
            {
                actors = value;
            }
        }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}