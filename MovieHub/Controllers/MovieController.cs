using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Details()
        {
            return View();
        }

        // GET: Movie/Add
        public ActionResult Add()
        {
            return View();
        }
    }
}