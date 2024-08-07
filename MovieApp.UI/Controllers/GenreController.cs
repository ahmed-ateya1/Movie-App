using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.ServicesContracts;

namespace MovieApp.UI.Controllers
{
    [Route("[controller]/[action]")]
    public class GenreController : Controller
    {
        private readonly IGenreServices _genreServices;

        public GenreController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public IActionResult UploadFromExcel()
        { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFromExcel(IFormFile excelFile)
        {
            if(excelFile == null || excelFile.Length == 0)
            {
                ViewBag.ErrorMessage = "Please Select xlsx file";
                return View();
            }
            if(!Path.GetExtension(excelFile.FileName).Equals(".xlsx",StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ErrorMessage = "Unsupported file. 'xlsx' file is expected";
                return View();
            }
            int genresInserted = await _genreServices.UploadGenresFromExcelFile(excelFile);
            ViewBag.Message = $"{genresInserted} Genre uploaded";

            return View();
        }
    }
}
