namespace MovieHub.Models.DTOs
{
    using Newtonsoft.Json;
    using System;

    public class MovieDTO
    {
        public string Title { get; set; }

        //public int Year { get; set; }

        public string Rated { get; set; }

        public DateTime Released { get; set; }

        public string Runtime { get; set; }

        [JsonProperty(PropertyName = "Genre")]
        public string Genres { get; set; } // Many to many

        [JsonProperty(PropertyName = "Director")]
        public string DirectorName { get; set; } // One to Many

        [JsonProperty(PropertyName = "Actors")]
        public string ActorNames { get; set; } // Many to many

        public string Plot { get; set; }

        [JsonProperty(PropertyName = "Language")]
        public string Languages { get; set; } // There could be more than 1.

        public string Awards { get; set; }

        public string Poster { get; set; } // This will be a link. Still no idea how to implement this!?

        public double ImdbRating { get; set; }

        //public long imdbVotes { get; set; }

        public string Production { get; set; } // One to many.
    }
}