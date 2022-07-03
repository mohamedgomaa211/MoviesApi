using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoivesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private new List<string> _allowedExtentions = new List<string>() { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;

        public MoivesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {

            var moives = await _context.moives.
                Include(m => m.Genre)
                .OrderByDescending(x => x.Rate)
                .Select(m => new MoivesDetailsDto
                {
                    Id = m.Id,
                    GenreId = m.GenreId,
                    Title = m.Title,
                    year = m.year,
                    StoryLine = m.StoryLine,
                    Rate = m.Rate,
                    GenreName = m.Genre.Name,
                    Poster = m.Poster,

                })
                .ToListAsync();
            return Ok(moives);

        }
        [HttpGet("id")]

        public async Task<IActionResult> GetByIdAsync(int id)
        {

            var moive = await _context.moives.FindAsync(id);
            if (moive == null)
            {
                return BadRequest($"No {id} found");

            }
            return Ok(moive);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MoivesInputDto moivesInputDto)
        {
            if (!_allowedExtentions.Contains(Path.GetExtension(moivesInputDto.Poster.FileName).ToLower()))
                return BadRequest("only .png and jpg is allowed!");
            if (moivesInputDto.Poster.Length > _MaxAllowedPosterSize)
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
                GenreId = moivesInputDto.GenreId,



            };
            await _context.moives.AddAsync(moive);

            _context.SaveChanges();
            return Ok(moive);

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MoivesInputDto moivesInputDto)
        {
            var data = await _context.moives.FindAsync( id);

            if (moivesInputDto.Poster == null)
            {
                return BadRequest("poster is required");
            }
            if (data == null) return BadRequest($"there is no entity with id {id}");

            data.Title = moivesInputDto.Title;
            data.StoryLine = moivesInputDto.StoryLine;
            data.Rate = moivesInputDto.Rate;
            data.year = moivesInputDto.year;
            data.GenreId = moivesInputDto.GenreId;
            if(moivesInputDto.Poster is not null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(moivesInputDto.Poster.FileName).ToLower()))
                    return BadRequest("only .png and jpg is allowed!");
                if (moivesInputDto.Poster.Length > _MaxAllowedPosterSize)
                    return BadRequest("the size is more than 1 mb");
                using var datastream = new MemoryStream();
                await moivesInputDto.Poster.CopyToAsync(datastream);
                data.Poster = datastream.ToArray();
            }

            var vaildGenre=_context.moives.AnyAsync(x => x.Id == moivesInputDto.GenreId);
            if (vaildGenre == null)
                return BadRequest("invaildGenreId");
            _context.SaveChanges();
            return Ok(data);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsnc(int id)
        {
            var moive = await _context.moives.FindAsync(id);
            if (moive == null)
            {
                return NotFound($"there is no moive with id ={id}");

            }
            _context.Remove(moive);
            _context.SaveChanges();
            return Ok(moive);
        }
    }
}
