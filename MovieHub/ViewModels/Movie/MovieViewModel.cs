namespace MovieHub.ViewModels.Movie
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class MovieViewModel
    {
        [DisplayName("* Title")]
        [Required]
        public string Title { get; set; }

        public string Rated { get; set; }

        public DateTime? Released { get; set; }

        public int? Runtime { get; set; }

        public string Genres { get; set; }

        public string DirectorName { get; set; }

        public string ActorNames { get; set; }

        public string Plot { get; set; }

        public string Languages { get; set; }

        public string Awards { get; set; }

        public string Poster { get; set; }

        public double ImdbRating { get; set; }

        public string Production { get; set; }
    }
}
