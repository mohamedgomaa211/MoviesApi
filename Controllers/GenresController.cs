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
        public async Task<IActionResult> GetResultAsync()
        {
            var data = await _context.Genres.ToListAsync();

            return Ok(data);
        }

    }
}
