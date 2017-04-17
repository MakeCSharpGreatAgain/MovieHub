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
        public bool DeleteReviewById(int id)
        {
            using (MovieDbContext context = new MovieDbContext())
            {
                Review review = context.Reviews.FirstOrDefault(r => r.Id == id);
                if (review == null)
                {
                    return false;
                }

                context.Reviews.Remove(review);
                context.SaveChanges();
                return true;
            }
        }

        public Review FetchReviewById(int? id)
        {
            using (var db = new MovieDbContext())
            {
                return db.Reviews.Where(r => r.Id == id).FirstOrDefault();
            }
        }
    }
}
