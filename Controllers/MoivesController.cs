using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoivesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private new List<string> _allowedExtentions= new List<string>() { ".jpg",".png"};
        private long _MaxAllowedPosterSize = 1048576;

        public MoivesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task <IActionResult> CreateAsync( [FromForm]MoivesInputDto moivesInputDto)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(moivesInputDto.Poster.FileName).ToLower()))
                return BadRequest("only .png and jpg is allowed!");
            if (moivesInputDto.Poster.Length> _MaxAllowedPosterSize)
                   return BadRequest("the size is more than 1 mb");

            using var datastream = new MemoryStream();

            await moivesInputDto.Poster.CopyToAsync(datastream);

            var moive = new Moive()
            {
                Title = moivesInputDto.Title,
                Rate = moivesInputDto.Rate,
                StoryLine = moivesInputDto.StoryLine,
                year = moivesInputDto.year,
                Poster = datastream.ToArray(),
                //GenreId = moivesInputDto.GenreId,



            };
            await _context.moives.AddAsync(moive);

            _context.SaveChanges();
            return Ok(moive);

        }
    }
}
