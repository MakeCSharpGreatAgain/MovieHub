using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content  { get; set; }

        public virtual Movie Movie { get; set; }
        public int MovieId { get; set; }

        public virtual  ApplicationUser Author { get; set; }

        public string AuthorId { get; set; }

        public bool isAuthor(string name)
        {
            return this.Author.UserName.Equals(name);
        }

    }
}
