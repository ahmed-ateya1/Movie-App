using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.DTO;
using MovieApp.Core.Enumerator;
using MovieApp.Core.Services;
using MovieApp.Core.ServicesContracts;
using MovieApp.Infrastructure.ApplicationDbContext;

namespace MovieApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class MovieController : Controller
    {
        private readonly IMovieServices _movieServices;
        private readonly IGenreServices _genreServices;
        private readonly IFavouriteServices _favouriteServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _db;
        public MovieController(IMovieServices movieServices , 
            IGenreServices genreServices,
            UserManager<ApplicationUser> userManager,
            IFavouriteServices favouriteServices,
            AppDbContext db)
        {
            _movieServices = movieServices;
            _genreServices = genreServices;
            _userManager = userManager;
            _favouriteServices = favouriteServices;
            _db = db;
        }

        [HttpGet]
        [Route("/")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchString, string sortBy = nameof(Movie.Rating), string sorted = "DESC")
        {
            IEnumerable<MovieResponse> movies = await _movieServices.GetAllMovies();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = await _movieServices.GetFilteredMovies(nameof(MovieAddRequest.Title), searchString);
            }

            ViewBag.currentSearchString = searchString;
            ViewBag.currentSortBy = sortBy;
            ViewBag.currentSorted = sorted;

            ViewBag.sortByList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Title", Value = nameof(Movie.Title) },
                new SelectListItem { Text = "Rating", Value = nameof(Movie.Rating) },
                new SelectListItem { Text = "Release Date", Value = nameof(Movie.ReleaseDate) }
             };

            List<MovieResponse>moviesSorted = (await _movieServices.SortedMovies(movies.ToList(), sortBy, sorted == "ASC" ? SortedOption.ASC : SortedOption.DESC)).ToList();

            return View(moviesSorted);
        }

        [HttpGet]
        [Authorize(Roles =nameof(UserOption.ADMIN))]
        public async Task<IActionResult> Create()
        {
            var genres = await _genreServices.GetAllGenre();

            ViewBag.Genres = genres.Select(x =>
                new SelectListItem() { Text = x.GenreName, Value = x.GenreID.ToString() }
            ).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserOption.ADMIN))]
        public async Task<IActionResult> Create(MovieAddRequest movieAddRequest)
        {
            if(!ModelState.IsValid)
            {
                var genres = await _genreServices.GetAllGenre();
                ViewBag.Genres = genres.Select(x =>
                    new SelectListItem() { Text = x.GenreName, Value = x.GenreID.ToString() }
                ).ToList();
                ViewBag.Errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage);
                return View(movieAddRequest);
            }
          
            await _movieServices.AddMovie(movieAddRequest);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(Guid? movieID)
        {
            MovieResponse movie = await _movieServices.GetMovieByMovieID(movieID);

            return View(movie);
        }
        [HttpGet]
        [Authorize(Roles = nameof(UserOption.ADMIN))]
        public async Task<IActionResult> Edit(Guid? movieID)
        {
            var movieBefore = await _movieServices.GetMovieByMovieID(movieID);
            var genres = await _genreServices.GetAllGenre();
            if (movieBefore == null)
                return NotFound();

            ViewBag.Genres = genres.Select(x =>
                new SelectListItem() { Text = x.GenreName, Value = x.GenreID.ToString() }
            ).ToList();
            var movieUpdateRequest = new MovieUpdateRequest()
            {
                Title = movieBefore.Title,
                Description = movieBefore.Description,
                GenreID = movieBefore.GenreID,
                ImageURL = movieBefore.ImageURL,
                MovieID = movieBefore.MovieID,
                Rating = movieBefore.Rating,
                ReleaseDate = movieBefore.ReleaseDate
            };

            return View(movieUpdateRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserOption.ADMIN))]
        public async Task<IActionResult> Edit(MovieUpdateRequest movieUpdate)
        {
            if (!ModelState.IsValid)
            {
                var genres = await _genreServices.GetAllGenre();
                ViewBag.Genres = genres.Select(x =>
                    new SelectListItem() { Text = x.GenreName, Value = x.GenreID.ToString() }
                ).ToList();
                ViewBag.Errors = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage);
                return View(movieUpdate);
            }
            await _movieServices.UpdateMovies(movieUpdate);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = nameof(UserOption.ADMIN))]
        public async Task<IActionResult>Delete(Guid? movieID)
        {
            await _movieServices.DeleteMovieByMovieID(movieID);
            return RedirectToAction("Index");
        }

    }
}
