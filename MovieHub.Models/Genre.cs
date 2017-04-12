using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieHub.Models
{
    public class Genre
    {
        private ICollection<Movie> movies;

        public Genre()
        {
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [JsonProperty(PropertyName = "Genre")]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies
        {
            get
            {
                return movies;
            }
            set
            {
                movies = value;
            }
        }
    }
}