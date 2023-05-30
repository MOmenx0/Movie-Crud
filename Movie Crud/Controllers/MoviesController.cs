using Car_Works.Models;
using Car_Works.Repositories;
using Car_Works.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Works.Controllers
{
    public class MoviesController : Controller
    {
        private long _maxAllowedPosterSize = 1048576;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        readonly IMoviesRepositorie moviesRepo  ;
        public MoviesController(IMoviesRepositorie _moviesRepo)
        {
            moviesRepo=_moviesRepo ;
        }
        public IActionResult Index()
        {
            return View(moviesRepo.GetAllMovies());
        }
        [HttpGet]
        public IActionResult create()
        {
            var viewmodel = new MovieViewModel();
            viewmodel.Genres = moviesRepo.GetAllGenres().Genres.OrderBy(a=>a.Name).ToList();

            return View(viewmodel);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult create(MovieViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Genres = moviesRepo.GetAllGenres().Genres.OrderBy(a => a.Name).ToList();
                return View("create", viewmodel);
            }
            var files = Request.Form.Files;

            if (!files.Any())
            {
                viewmodel.Genres = moviesRepo.GetAllGenres().Genres.OrderBy(a => a.Name).ToList();
                ModelState.AddModelError("Poster", "Please select movie poster!");
                return View("Create", viewmodel);
            }

            var poster = files.FirstOrDefault();

            if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                viewmodel.Genres = moviesRepo.GetAllGenres().Genres.OrderBy(a => a.Name).ToList();
                ModelState.AddModelError("Poster", "Only .PNG, .JPG images are allowed!");
                return View("create", viewmodel);
            }

            if (poster.Length > _maxAllowedPosterSize)
            {
                viewmodel.Genres = moviesRepo.GetAllGenres().Genres.OrderBy(a => a.Name).ToList();
                ModelState.AddModelError("Poster", "Poster cannot be more than 1 MB!");
                return View("Create", viewmodel);
            }
            using var dataStream = new MemoryStream();
            poster.CopyTo(dataStream);
            viewmodel.Poster = dataStream.ToArray();


            moviesRepo.AddMovie(viewmodel);

            return RedirectToAction("index");

        }

    }
}
