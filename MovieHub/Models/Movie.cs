using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Movie // This will later become MovieDTO.cs (just as it is), and this fail will change, in order to pass correctly with the other models.
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public string Rated { get; set; }

        public DateTime Released { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; } // This will become freign key. Many to many

        public string Director { get; set; } // This will be a foreign key. One to Many

        public string Actors { get; set; } // This will be foreign key. Many to many

        public string Plot { get; set; }

        public string Language { get; set; } // There could be more than 1.

        public string Awards { get; set; }

        // public string Poster { get; set; } // This will be a link. Still no idea how to implement this!?

        public float imdbRating { get; set; }

        public long imdbVotes { get; set; }

        public string Production { get; set; } // This will be a foreign key. Many to many.
    }
}