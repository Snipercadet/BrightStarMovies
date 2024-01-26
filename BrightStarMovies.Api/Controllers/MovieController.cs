using Azure;
using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Application.Interfaces;
using BrightStarMovies.Domain.Common.Request;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace BrightStarMovies.Api.Controllers
{
    [ApiController]
    [Route("api/movie")]
    public class MovieController(IMovieService movie) : ControllerBase
    {
        [HttpPost("create-movie")]
        public async Task<IActionResult> CreateMovie(MovieDto model)
        {
            var result = await movie.AddMovies(model);
            if (result.Data == null)
            {
                return Ok(result);
            }
            return CreatedAtAction(nameof(GetMovie), new { id = result.Data.Id }, result);
        }

        [HttpPost("add-movie-to-genre/{id}")]
        public async Task<IActionResult> AddMovieToGenre([FromRoute] Guid id, List<Guid> genreId)
        {
            var result = await movie.AddMovieToGenre(id,genreId);
            return Ok(result);
        }

        [HttpPost("remove-movie-from-genre/{id}")]
        public async Task<IActionResult> RemoveMovieFromGenre([FromRoute] Guid id, List<Guid> genreId)
        {
            var result = await movie.RemoveMovieFromGenre(id, genreId);
            return Ok(result);
        }

        [HttpPut("update-movie/id")]
        public async Task<IActionResult> UpdateMovie([FromQuery] Guid id, MovieDto model)
        {
            var result = await movie.UpdateMovie(id, model);
            return Ok(result);
        }

        [HttpDelete("delete-movie/id")]
        public async Task<IActionResult> DeleteMovie([FromQuery] Guid id)
        {
            var result = await movie.DeleteMovie(id);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMovie([FromQuery] Guid id)
        {
            var result = await movie.GetMovie(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies([FromQuery] PaginationData model)
        {
            var result = await movie.GetMovies(model);
            return Ok(result);
        }
    }
}
