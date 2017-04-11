﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieHub.Models
{
    public class Production
    {
        public Production()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}