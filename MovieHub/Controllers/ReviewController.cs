using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieHub.Data;
using MovieHub.Services;
using MovieHub.Services.Interfaces;
using MovieHub.ViewModels.Movie;

namespace MovieHub.Controllers
{
    public class ReviewController : Controller
    {
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        // GET: Movie/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            IReviewService reviewService = ServiceLocator.Instance.GetService<IReviewService>();
            var review = reviewService.FetchReviewById(id);

            return View(review);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ReviewViewModel reviewModel)
        {
            if (ModelState.IsValid && reviewModel != null)
            {

                using (var db = new MovieDbContext())
                {
                    var review = db.Reviews.FirstOrDefault(r=>r.Id==reviewModel.Id);

                    review.Content = reviewModel.Content;
                    db.Entry(review).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", "Movie", new {id = review.MovieId});
                } 
            }

            return View();

        }

    }
}