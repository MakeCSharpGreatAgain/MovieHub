namespace MovieHub.ViewModels.Movie
{
    using System;

    public class ListAllViewModel
    {
        //private DateTime? releasedDate;
        public int Id { get; set; }

        public string PosterUrl { get; set; }

        public string Title { get; set; }

        public int? ReleasedYear { get; set; }

        public double? ImdbRating { get; set; }
    }
}