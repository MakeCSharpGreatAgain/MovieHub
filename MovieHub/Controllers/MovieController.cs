﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MovieHub.Data;
using MovieHub.Services.Interfaces;
using MovieHub.Services;
using MovieHub.Authorize;
using MovieHub.Models;
using MovieHub.Models.DTOs;
using MovieHub.ViewModels.Movie;

namespace MovieHub.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie -- Used only to redirect to another view.
        public ActionResult Index()
        {
            return RedirectToAction("ListAll");
        }

        //GET: Movie/ListAll
        public ActionResult ListAll()
        {
            //using (var db = new MovieDbContext())
            //{
            //    var movies = db.Movies
            //        .ToList()
            //        .OrderByDescending(m => m.ImdbRating)
            //        .ToList();

            //}
            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();

            return View(movieService.GetAllMovies());
        }

        // GET: Movie/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            using (var db = new MovieDbContext())
            {
                //TODO : Include more things if needed 
                var movie = db.Movies
                    .Where(m => m.Id == id)
                    .Include(m => m.Director)
                    .Include(m => m.Actors)
                    .Include(m => m.Production)
                    .Include(m => m.Genres)
                    .Include(m=>m.Reviews.Select(r=>r.Author))
                    .First();

                return View(movie);

            }
           // IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();
          //  Movie movie = movieService.GetMovieById((int)id);

            //if (movie == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(movie);

        }

        // GET: Movie/Create
        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieDTO movie)
        {
            return RedirectToAction("Details", "Movie", new { @id = 1 });
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitReview(ReviewViewModel reviewModel)
        {
            if (ModelState.IsValid && reviewModel != null)
            {
                using (var db = new MovieDbContext())
                {

                    var userId = this.User.Identity.GetUserId();

                    db.Reviews.Add(new Review()
                    {
                        AuthorId = userId,
                        Content = reviewModel.Content,
                        MovieId = reviewModel.MovieId
                    });

                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = reviewModel.MovieId });
                }
            }
            return RedirectToAction("Details", new { id = reviewModel.MovieId });
        }

    }
}