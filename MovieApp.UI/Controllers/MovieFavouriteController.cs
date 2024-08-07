using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieApp.Core.Domain.Entites;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.DTO;
using MovieApp.Core.Enumerator;
using MovieApp.Core.ServicesContracts;

public class MovieFavouriteController : Controller
{
    private readonly IMovieFavouriteServices _movieFavouriteServices;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMovieServices _movieServices;
    private readonly IFavouriteServices _favouriteServices;

    public MovieFavouriteController(IMovieFavouriteServices movieFavouriteServices,
        UserManager<ApplicationUser> userManager,
        IMovieServices movieServices,
        IFavouriteServices favouriteServices)
    {
        _movieFavouriteServices = movieFavouriteServices ;
        _userManager = userManager ;
        _movieServices = movieServices;
        _favouriteServices = favouriteServices;
    }
    [HttpGet]
    public async Task<IActionResult> Index(string? searchString, string sortBy = nameof(Movie.Rating), string sorted = "DESC")
    {
        var user = await _userManager.GetUserAsync(User);
        if(user == null) 
            return Unauthorized();

        IEnumerable<MovieResponse> movies = await _movieFavouriteServices
            .GetFavouriteMoviesByUser(user.Id);

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

        List<MovieResponse> moviesSorted = (await _movieServices.SortedMovies(movies.ToList(), sortBy, sorted == "ASC" ? SortedOption.ASC : SortedOption.DESC)).ToList();

        return View(moviesSorted);
    }

    [HttpGet]
    public async Task<IActionResult> AddMovieToUser(Guid? movieID)
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return Unauthorized();

        var movie = await _movieServices.GetMovieByMovieID(movieID);

        if (movie == null)
            return NotFound();

        bool haveFavourite = await _favouriteServices.HaveFavourite(user.Id);
        bool moviefound = await _movieFavouriteServices.IsMovieInUserFavourites(movieID, user.Id);
        if (moviefound)
        {
            TempData["Message"] = "Movie is already added to favorites.";
            return RedirectToAction("Index");
        }

        MovieFavourite movieFavourite = new MovieFavourite
        {
            MovieID = movieID.Value
        };

        if (!haveFavourite)
        {
            var favouriteAddRequest = new FavouriteAddRequest { UserID = user.Id };
            var favouriteResponse = await _favouriteServices.CreateFavourite(favouriteAddRequest);
            movieFavourite.FavouriteID = favouriteResponse.FavouriteID;
        }
        else
        {
            var favourite = await _favouriteServices.GetFavouriteByUserId(user.Id);
            movieFavourite.FavouriteID = favourite.FavouriteID;
        }

        await _movieFavouriteServices.AddMovieToFavourite(movieFavourite);
        TempData["Message"] = "Movie added to favorites successfully.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> RemoveMovieFromUser(Guid? movieID)
    {
        var user =await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var movie = await _movieServices.GetMovieByMovieID(movieID);
        var favourite = await _favouriteServices.GetFavouriteByUserId(user.Id);
        if(movie == null)
            return NotFound();

        await _movieFavouriteServices.DeleteMovieFromFavourite(movieID.Value, favourite.FavouriteID);

        return RedirectToAction("Index");
    }

}
