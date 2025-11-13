using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN232_PE_FA25_LeVoBaoDuy.Data;
using PRN232_PE_FA25_LeVoBaoDuy.Models;

namespace PRN232_PE_FA25_LeVoBaoDuy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MovieController(ApplicationDbContext context) => _context = context;

        // GET: api/Movie?search=abc&genre=Action&sortBy=title|rating&sortOrder=asc|desc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(
            [FromQuery] string? search, 
            [FromQuery] string? genre,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder)
        {
            IQueryable<Movie> q = _context.Movies.AsNoTracking();

            // Search by title
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(m => m.Title.Contains(search));

            // Filter by genre
            if (!string.IsNullOrWhiteSpace(genre))
                q = q.Where(m => m.Genre == genre);

            // Sort
            sortBy = sortBy?.ToLower() ?? "title";
            sortOrder = sortOrder?.ToLower() ?? "asc";

            q = (sortBy, sortOrder) switch
            {
                ("rating", "desc") => q.OrderByDescending(m => m.Rating ?? 0).ThenBy(m => m.Title),
                ("rating", _) => q.OrderBy(m => m.Rating ?? 0).ThenBy(m => m.Title),
                ("title", "desc") => q.OrderByDescending(m => m.Title),
                _ => q.OrderBy(m => m.Title)
            };

            return await q.ToListAsync();
        }

        // GET: api/Movie/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            return movie is null ? NotFound() : movie;
        }

        // POST: api/Movie
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            movie.Id = 0;
            movie.CreatedAt = DateTime.UtcNow;

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/Movie/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (id != movie.Id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var dbMovie = await _context.Movies.FindAsync(id);
            if (dbMovie is null) return NotFound();

            dbMovie.Title = movie.Title.Trim();
            dbMovie.Genre = string.IsNullOrWhiteSpace(movie.Genre) ? null : movie.Genre.Trim();
            dbMovie.Rating = movie.Rating;
            dbMovie.PosterImage = string.IsNullOrWhiteSpace(movie.PosterImage) ? null : movie.PosterImage.Trim();

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Movie/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie is null) return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Movie/genres
        [HttpGet("genres")]
        public async Task<ActionResult<IEnumerable<string>>> GetGenres()
        {
            var genres = await _context.Movies
                .Where(m => m.Genre != null)
                .Select(m => m.Genre!)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();
            return genres;
        }
    }
}

