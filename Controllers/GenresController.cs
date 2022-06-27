using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {


       readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        //public GenresController(ApplicationDbContext context)
        //{

        //    _context = context;

        //}
        [HttpGet]
        public async Task<IActionResult> GetResultAsync()
        {
            var data = await _context.Genres.OrderBy(x=>x.Name).ToListAsync();

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreInputDto genreInputDto)
        {

            var genre = new Genre()
            {
                Name = genreInputDto.Name,
            };
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return Ok(genre);

        }
        [HttpPut("id")]

        public async Task<IActionResult> UpdateAsync(int id ,[FromBody]  GenreInputDto genreInputDto)
        {
           var genre= await _context.Genres.SingleOrDefaultAsync(g=>g.Id==id);
            if (genre == null)
            {

                return NotFound($"no genre was found with {id}");
            }
            genre.Name = genreInputDto.Name;
            _context.SaveChanges();

            return Ok(genre);

        }
        [HttpDelete("id")]

        public async Task<IActionResult> DeleteAsync(int id, [FromBody] GenreInputDto genreInputDto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (genre == null)
            {

                return NotFound($"no genre was found with {id}");
            }
            _context.Remove(genre);

            _context.SaveChanges();

            return Ok(genre);

        }



    }
}
