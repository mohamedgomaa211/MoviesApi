using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services.Interfaces;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoivesController : ControllerBase
    {
        private readonly IMoivesService moivesService;
        readonly IGenresService genresService;
        private readonly IMapper _mapper;


        private new List<string> _allowedExtentions = new List<string>() { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;

        public MoivesController(IMoivesService moivesService, IGenresService genresService, IMapper mapper)
        {
            this.moivesService = moivesService;
            this.genresService = genresService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(byte genreId)
        {

            var moives = await moivesService.GetAll( genreId);
            var data = _mapper.Map<IEnumerable<MoivesDetailsDto>>(moives);
            return Ok(data);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int  id)
        {
            var moive = await moivesService.GetMoiveByIdAsync(id);
            if(moive == null)
            {
                return NotFound();
            }
            var dto = _mapper.Map<MoivesDetailsDto>(moive);
            return Ok(dto);

        }

        [HttpGet("GetByGenreId")]

        public async Task<IActionResult> GetByGenreIdAsync(byte id)
        {

            var moives = await moivesService.GetAll(id);
            var data = _mapper.Map<IEnumerable<MoivesDetailsDto>>(moives);

            return Ok(data);
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
            var isValidGenre = await genresService.GetMoiveGenre(moivesInputDto.GenreId);

            var movie = _mapper.Map<Moive>(moivesInputDto);

            moivesService.Add(movie);

            return Ok(movie);

        }
        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MoivesInputDto moivesInputDto)
        {
            var data = await moivesService.GetMoiveByIdAsync(id);

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

            var vaildGenre = genresService.GetMoiveGenre(moivesInputDto.GenreId);
            if (vaildGenre == null)
                return BadRequest("invaildGenreId");
            moivesService.Update(data);
            return Ok(data);

        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsnc(int id)
        {
            var moive = await moivesService.GetMoiveByIdAsync(id);
            if (moive == null)
            {
                return NotFound($"there is no moive with id ={id}");

            }
            moivesService .Delete(moive);
            return Ok(moive);
        }
    }
}
