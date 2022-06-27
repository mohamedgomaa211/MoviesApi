using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoivesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoivesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync( [FromForm]MoivesInputDto moivesInputDto)
        {
            using var datastram = new MemoryStream();
            await moivesInputDto.Poster.CopyToAsync(datastram);

            var moive = new Moive()
            {
                Title = moivesInputDto.Title,
                Rate = moivesInputDto.Rate,
                StoryLine = moivesInputDto.StoryLine,
                year = moivesInputDto.year,
                Poster = datastram.ToArray(),
                GenreId = moivesInputDto.GenreId,



            };
            await _context.moives.AddAsync(moive);

            _context.SaveChanges();


        }
    }
}
