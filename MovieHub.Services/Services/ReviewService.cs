using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHub.Data;
using MovieHub.Models;
using MovieHub.Services.Interfaces;

namespace MovieHub.Services.Services
{
    public class ReviewService : IReviewService
    {
        public Review FetchReviewById(int? id)
        {
            using (var db = new MovieDbContext())
            {
                return db.Reviews.FirstOrDefault(r=>r.Id==id);
            }
        }
    }
}
