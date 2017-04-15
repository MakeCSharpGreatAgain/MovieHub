﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieHub.Data;
using MovieHub.Services.Interfaces;
using MovieHub.Services;

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
                    .Where(m=>m.Id == id)
                    .Include(m=>m.Director)
                    .Include(m=>m.Actors)
                    .Include(m=>m.Production)
                    .Include(m=>m.Genres)
                    .First();


                if (movie == null)
                {
                    return HttpNotFound();
                }

                return View(movie);
            }

        }

        // GET: Movie/Add
        public ActionResult Add()
        {
            return View();
        }
    }
}