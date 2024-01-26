using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Domain.Common.Request;
using BrightStarMovies.Domain.Common.Response;
using BrightStarMovies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Application.Interfaces
{
    public interface IMovieService
    {
        Task<BaseResponse<GetMovieResultDto>> AddMovies(MovieDto request);
        Task<PaginatedResponse<GetMovieResultDto>> GetMovies(PaginationData request);
        Task<BaseResponse<GetMovieResultDto>> GetMovie(Guid id);
        Task<BaseResponse<GetMovieResultDto>> UpdateMovie(Guid id, MovieDto model);
        Task<BaseResponse<GetMovieResultDto>> DeleteMovie(Guid id);
        Task<BaseResponse<GetMovieResultDto>> AddMovieToGenre(Guid movieId, List<Guid> genreIds);
        Task<BaseResponse<GetMovieResultDto>> RemoveMovieFromGenre(Guid movieId, List<Guid> genreIds);

    }
}
