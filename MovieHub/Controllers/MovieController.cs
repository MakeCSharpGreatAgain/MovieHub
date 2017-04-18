namespace MovieHub.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using MovieHub.Utils;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using MovieHub.Data;
    using MovieHub.Services.Interfaces;
    using MovieHub.Services;
    using MovieHub.Authorize;
    using MovieHub.Models;
    using MovieHub.ViewModels.Movie;
    using AutoMapper;

    public class MovieController : Controller
    {
        // GET: Movie -- Used only to redirect to another view.
        public ActionResult Index()
        {
            return RedirectToAction("ListAll");
        }

        //GET: Movie/ListAll
        public ActionResult ListAll(string search)
        {
            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();
            ICollection<Movie> movies = movieService.SearchMoviesByTitle(search);
            ICollection<ListAllViewModel> viewModels = Mapper.Map<ICollection<ListAllViewModel>>(movies);

            return View(viewModels);
        }

        // GET: Movie/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();
            Movie movie = movieService.GetMovieById((int)id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);

        }

        // GET: Movie/Create
        [CustomAuthorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            //Check if we have error messages from our last attempt to create a movie
            if (TempData["message"] != null)
            {
                ViewBag.Message = TempData["message"].ToString();
            }
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(CreateViewModel movieViewModel)
        {

            if (!ModelState.IsValid)
            {
                //if the create process failed, this message will be displayed 
                TempData["message"] = "Please make sure you aren't leaving any empty fields marked as required.";
                return RedirectToAction("Create");
            }

            //MovieDTO movieDTO = Mapper.Map<MovieViewModel, MovieDTO>(movieViewModel);

            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();

            Movie movie = new Movie()
            {
                Awards = movieViewModel.Awards,
                Title = movieViewModel.Title,
                PosterUrl = movieViewModel.Poster,
                ImdbRating = movieViewModel.ImdbRating,
                Rated = movieViewModel.Rated,
                Languages = movieViewModel.Languages,
                Released = movieViewModel.Released,
                Plot = movieViewModel.Plot,
                Runtime = movieViewModel.Runtime
            };

            if (!string.IsNullOrEmpty(movieViewModel.ActorNames))
            {
                ICollection<Actor> actors = Utils.GetActors(movieViewModel.ActorNames);
                movie.Actors = actors;
            }

            if (!string.IsNullOrEmpty(movieViewModel.Genres))
            {
                ICollection<Genre> genres = Utils.GetGenres(movieViewModel.Genres);
                movie.Genres = genres;
            }

            if (!string.IsNullOrEmpty(movieViewModel.DirectorName))
            {
                Director director = Utils.GetDirector(movieViewModel);

                movie.Director = director;
            }

            if (!string.IsNullOrEmpty(movieViewModel.Production))
            {
                Production production = Utils.GetProduction(movieViewModel);

                movie.Production = production;
            }

            int id = movieService.AddMovie(movie);
            TempData["message"] = null;

            return RedirectToAction("Details", new { @id = id });
        }

        [Authorize]
        [HttpPost]
        public ActionResult SubmitReview(ReviewViewModel reviewModel)
        {
            if (ModelState.IsValid && reviewModel != null)
            {
                var userId = this.User.Identity.GetUserId();

                Review review = new Review()
                {
                    AuthorId = userId,
                    Content = reviewModel.Content,
                    MovieId = reviewModel.MovieId
                };

                IReviewService reviewService = ServiceLocator.Instance.GetService<IReviewService>();
                reviewService.AddReview(review);

                return RedirectToAction("Details", new { id = reviewModel.MovieId });
            }

            return RedirectToAction("Details", new { id = reviewModel.MovieId });
        }

    }
}