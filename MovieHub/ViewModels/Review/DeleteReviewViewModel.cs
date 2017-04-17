namespace MovieHub.ViewModels.Review
{
    using System;

    public class DeleteReviewViewModel
    {
        public int ReviewId { get; set; }

        public string Content { get; set; }

        public int MovieId { get; set; }
    }
}