using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Domain.Common.Request;
using BrightStarMovies.Domain.Common.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Application.Interfaces
{
    public interface IGenreService
    {
        Task<BaseResponse<GetGenreResultDto>> AddGenre(GenreDto request);
        Task<PaginatedResponse<GetGenreResultDto>> GetGenres(PaginationData request);
        Task<BaseResponse<GetGenreResultDto>> GetGenre(Guid id);
        Task<BaseResponse<GetGenreResultDto>> UpdateGenre(Guid id, GenreDto model);
        Task<BaseResponse<GetMovieResultDto>> DeleteGenre(Guid id);
    }
}
