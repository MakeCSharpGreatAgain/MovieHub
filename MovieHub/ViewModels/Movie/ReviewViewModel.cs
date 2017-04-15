using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieHub.ViewModels.Movie
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        public string AuthorName { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsAuthor(string name)
        {
            return this.AuthorName.Equals(name);
        }
    }
}