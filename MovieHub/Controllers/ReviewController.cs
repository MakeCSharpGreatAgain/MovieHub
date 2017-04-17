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
using MovieHub.Models;
using MovieHub.ViewModels.Review;
using AutoMapper;
using MovieHub.Authorize;

namespace MovieHub.Controllers
{
    public class ReviewController : Controller
    {
        //GET: Review/Delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            IReviewService reviewService = ServiceLocator.Instance.GetService<IReviewService>();
            Review review = reviewService.FetchReviewById(id);

            DeleteReviewViewModel viewModel = Mapper.Map<DeleteReviewViewModel>(review);

            return View(viewModel);
        }

        //POST: Review/Delete
        [HttpPost]
        public ActionResult Delete(DeleteReviewViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                IReviewService reviewService = ServiceLocator.Instance.GetService<IReviewService>();
                bool isSuccssfull = reviewService.DeleteReviewById(viewModel.ReviewId);

                if (!isSuccssfull)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }

            return RedirectToAction("Details", "Movie", new
            {
                @id = viewModel.ReviewId
            });
        }

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
                    var review = db.Reviews.FirstOrDefault(r => r.Id == reviewModel.Id);

                    review.Content = reviewModel.Content;
                    db.Entry(review).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details", "Movie", new { id = review.MovieId });
                }
            }

            return View();
        }

    }
}