using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Director
    {
        public Director()
        {
            this.DirectedMovies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Movie> DirectedMovies { get; set; }
    }
}