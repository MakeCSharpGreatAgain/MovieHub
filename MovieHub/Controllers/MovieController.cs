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
            //using (var db = new MovieDbContext())
            //{
            //    var movies = db.Movies
            //        .ToList()
            //        .OrderByDescending(m => m.ImdbRating)
            //        .ToList();

            //}
            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();

            return View(movieService.SearchMoviesByTitle(search));
        }

        // GET: Movie/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //using (var db = new MovieDbContext())
            //{
            //    TODO: Include more things if needed
            //    var movie = db.Movies
            //        .Where(m => m.Id == id)
            //        .Include(m => m.Director)
            //        .Include(m => m.Actors)
            //        .Include(m => m.Production)
            //        .Include(m => m.Genres)
            //        .Include(m => m.Reviews.Select(r => r.Author))
            //        .First();

            //    return View(movie);

            //}
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
        public ActionResult Create(MovieViewModel movieViewModel)
        {

            if (!ModelState.IsValid)
            {
                //if the create process failed, this message will be displayed 
                TempData["message"] = "Please make sure you aren't leaving any empty fields marked as required.";
                return RedirectToAction("Create");
            }

            //MovieDTO movieDTO = Mapper.Map<MovieViewModel, MovieDTO>(movieViewModel);

            IMovieService movieService = ServiceLocator.Instance.GetService<IMovieService>();
            //IActorService actorService = ServiceLocator.Instance.GetService<IActorService>();
            //IGenreService genreService = ServiceLocator.Instance.GetService<IGenreService>();
            //IDirectorService directorService = ServiceLocator.Instance.GetService<IDirectorService>();
            //IProductionService productionService = ServiceLocator.Instance.GetService<IProductionService>();

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

        //private static Production GetProduction(MovieViewModel movieViewModel)
        //{
        //    IProductionService productionService = ServiceLocator.Instance.GetService<IProductionService>();

        //    Production production = productionService.GetProductionByName(movieViewModel.Production);

        //    if (production == null)
        //    {
        //        production = productionService.InsertProduction(movieViewModel.Production);
        //    }

        //    return production;
        //}

        //private static Director GetDirector(MovieViewModel movieViewModel)
        //{
        //    IDirectorService directorService = ServiceLocator.Instance.GetService<IDirectorService>();

        //    Director director = directorService.GetDirectorByName(movieViewModel.DirectorName);

        //    if (director == null)
        //    {
        //        director = directorService.InsertDirector(movieViewModel.DirectorName);
        //    }

        //    return director;
        //}

        //private ICollection<Genre> GetGenres(string genreNames)
        //{
        //    IGenreService genreService = ServiceLocator.Instance.GetService<IGenreService>();

        //    ICollection<Genre> genres = new List<Genre>();
        //    string[] genresArray = genreNames.
        //        Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //        .Select(g => g.Trim())
        //        .ToArray();

        //    foreach (var name in genresArray)
        //    {
        //        if (!genres.Any(a => a.Name.ToLower() == name.ToLower()))
        //        {
        //            Genre genre = genreService.GetGenreByName(name);
        //            if (genre == null)
        //            {
        //                genre = genreService.InsertGenre(name);
        //            }

        //            genres.Add(genre);
        //        }
        //    }

        //    return genres;
        //}

        //private ICollection<Actor> GetActors(string actorNames)
        //{
        //    IActorService actorService = ServiceLocator.Instance.GetService<IActorService>();

        //    ICollection<Actor> actors = new List<Actor>();
        //    string[] actorsArray = actorNames
        //        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //        .Select(a => a.Trim())
        //        .ToArray();

        //    foreach (var name in actorsArray)
        //    {
        //        if (!actors.Any(a => a.Name.ToLower() == name.ToLower()))
        //        {
        //            Actor actor = actorService.GetActorByName(name);
        //            if (actor == null)
        //            {
        //                actor = actorService.InsertActor(name);
        //            }

        //            actors.Add(actor);
        //        }
        //    }

        //    return actors;
        //}

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