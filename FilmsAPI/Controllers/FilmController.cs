using FilmsAPI.Data;
using FilmsAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FilmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FilmController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }



        [HttpGet]
        public async Task<IEnumerable<Film>> GetFilms()
        {
            return await _context.Films.Select(x => new Film()
            {
                FilmID = x.FilmID,
                Name = x.Name,
                Description = x.Description,
                ReleaseDate = x.ReleaseDate,
                Genre = x.Genre,
                Rating = x.Rating,
                TicketPrice = x.TicketPrice,
                Country = x.Country,
                ImageURL = x.ImageURL,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageURL)

            }).ToListAsync();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Film>> GetFilmById(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            film.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, film.ImageURL);

            return film;
        }



        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromForm]Film film)
        {
            film.ImageURL = await SaveImage(film.ImageFile);
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return StatusCode(201);
        }



        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
