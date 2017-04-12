using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Production
    {
        private ICollection<Movie> movies;

        public Production()
        {
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [JsonProperty(PropertyName = "Production")]
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