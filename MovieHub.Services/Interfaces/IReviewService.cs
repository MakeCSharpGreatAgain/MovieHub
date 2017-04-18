using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHub.Models;

namespace MovieHub.Services.Interfaces
{
    public interface IReviewService
    {
        Review FetchReviewById(int? id);

        bool DeleteReviewById(int id);

        void AddReview(Review review);

        void UpdateReview(int reviewId, string newContent);
    }
}
