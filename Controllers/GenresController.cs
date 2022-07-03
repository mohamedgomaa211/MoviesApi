using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {


       readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        //public GenresController(ApplicationDbContext context)
        //{

        //    _context = context;

        //}
        [HttpGet]
        public async Task<IActionResult> GetResultAsync()
        {
            var data = await genresService.GetAll();

            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreInputDto genreInputDto)
        {

            var genre = new Genre()
            {
                Name = genreInputDto.Name,
            };
            await genresService.Add(genre);

            return Ok(genre);

        }
        [HttpPut("id")]

        public async Task<IActionResult> UpdateAsync(byte id ,[FromBody]  GenreInputDto genreInputDto)
        {

            var genre = await genresService.GetById(id);
            if (genre == null)
            {

                return NotFound($"no genre was found with {id}");
            }
            genre.Name = genreInputDto.Name;
            genresService.Update(genre);

            return Ok(genre);

        }
        [HttpDelete("id")]

        public async Task<IActionResult> DeleteAsync(byte id, [FromBody] GenreInputDto genreInputDto)
        {
            var genre = await genresService.GetById(id);
            if (genre == null)
            {

                return NotFound($"no genre was found with {id}");
            }
            genresService.Delete(genre);
          

            return Ok(genre);

        }



    }
}
