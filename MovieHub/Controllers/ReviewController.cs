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

            DeleteViewModel viewModel = Mapper.Map<DeleteViewModel>(review);

            return View(viewModel);
        }

        //POST: Review/Delete
        [HttpPost]
        public ActionResult Delete(DeleteViewModel viewModel)
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
                @id = viewModel.MovieId
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

            if (review == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EditViewModel viewModel = Mapper.Map<EditViewModel>(review);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(EditViewModel viewModel)
        {
            if (ModelState.IsValid && viewModel != null)
            {
                IReviewService reviewService = ServiceLocator.Instance.GetService<IReviewService>();

                reviewService.UpdateReview(viewModel.Id, viewModel.Content);

                return RedirectToAction("Details", "Movie", new { id = viewModel.MovieId });
            }

            return View();
        }
    }
}