using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Application.Interfaces;
using BrightStarMovies.Domain.Common.Request;
using BrightStarMovies.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarMovies.Api.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController(IGenreService genreService): ControllerBase
    {
        [HttpPost("create-genre")]
        public async Task<IActionResult> CreateGenre(GenreDto model)
        {
            var result = await genreService.AddGenre(model);
            return CreatedAtAction(nameof(GetGenre), new { id = result.Data.Id }, result);
        }

        [HttpPut("update-genre/id")]
        public async Task<IActionResult> UpdateGenre([FromQuery] Guid id, GenreDto model)
        {
            var result = await genreService.UpdateGenre(id, model);
            return Ok(result);
        }

        [HttpDelete("delete-genre/id")]
        public async Task<IActionResult> DeleteGenre([FromQuery] Guid id)
        {
            var result = await genreService.DeleteGenre(id);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetGenre([FromQuery] Guid id)
        {
            var result = await genreService.GetGenre(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetGenres([FromQuery] PaginationData model)
        {
            var result = await genreService.GetGenres(model);
            return Ok(result);
        }
    }
}
