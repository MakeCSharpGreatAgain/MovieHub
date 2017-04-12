using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Director
    {
        private ICollection<Movie> directedMovies;

        public Director()
        {
            this.directedMovies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [JsonProperty(PropertyName = "Director")]
        public string Name { get; set; }        

        public virtual ICollection<Movie> DirectedMovies
        {
            get
            {
                return directedMovies;
            }

            set
            {
                directedMovies = value;
            }
        }
    }
}