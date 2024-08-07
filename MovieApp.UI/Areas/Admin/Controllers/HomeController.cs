using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain.IdentityEntites;
using MovieApp.Core.Enumerator;
using MovieApp.Core.ServicesContracts;
using MovieApp.Infrastructure.ApplicationDbContext;
using MovieApp.UI.Areas.Admin.Data;
using System.Linq;

namespace MovieApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(UserOption.ADMIN))]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMovieFavouriteServices _movieFavouriteServices;
        private readonly AppDbContext _db;

        public HomeController(UserManager<ApplicationUser> userManager , 
            IMovieFavouriteServices movieFavouriteServices,
            AppDbContext db)
        {
            _movieFavouriteServices = movieFavouriteServices;
            _userManager = userManager;
            _db = db;
        }

        [Route("Admin/dashboard")]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var adminViewModel = new List<AdminViewModel>();

            foreach (var user in users)
            {
                var favoriteMoviesCount = await _movieFavouriteServices.GetFavouriteMoviesCountByUser(user.Id);
                adminViewModel.Add(new AdminViewModel
                {
                    Username = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    FavoriteMoviesCount = favoriteMoviesCount,
                    UserID = user.Id
                });
            }

            return View(adminViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> RemoveUser(Guid? userID)
        {
            if (userID == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userID.Value.ToString());

            if (user == null)
                return NotFound();

            var userFavourites = await _db.Favorites
                .Where(f => f.UserID == userID.Value).ToListAsync();

            if (userFavourites.Any())
            {
                _db.Favorites.RemoveRange(userFavourites);
                await _db.SaveChangesAsync();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove user.");
            }

            return RedirectToAction("Index");
        }

    }
}
