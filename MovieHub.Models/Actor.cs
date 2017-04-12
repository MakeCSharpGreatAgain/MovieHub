using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Actor
    {
        private ICollection<Movie> movies;
        public Actor()
        {
            this.movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

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