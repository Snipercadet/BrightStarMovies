using AutoMapper;
using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Application.Interfaces;
using BrightStarMovies.Domain.Common.Extensions;
using BrightStarMovies.Domain.Common.Request;
using BrightStarMovies.Domain.Common.Response;
using BrightStarMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrightStarMovies.Infrastructure.Services
{
    public class GenreService(IAppDbContext context, IMapper mapper) : IGenreService
    {
        public async Task<BaseResponse<GetGenreResultDto>> AddGenre(GenreDto request)
        {
            if (await context.Genres.AnyAsync(x => x.Name.Equals(request.Name)))
            {
                return new BaseResponse<GetGenreResultDto>("Genre already exist", false, null);
            }

            var genre = mapper.Map<Genre>(request);
            context.Genres.Add(genre);
            await context.SaveChangesAsync(default);
            var result = mapper.Map<GetGenreResultDto>(genre);
            return new BaseResponse<GetGenreResultDto>("Genre created successfully", true, result);
        }
        public async Task<PaginatedResponse<GetGenreResultDto>> GetGenres(PaginationData request)
        {
            var genre = await context.Genres.ToListAsync();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                genre = genre.Where(x => x.Name == request.SearchTerm).ToList();
            }

            if (request.FromDate.HasValue)
            {
                genre = genre.FilterFromDate(request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                genre = genre.FilterToDate(request.ToDate.Value);
            }
            var result = mapper.Map<List<GetGenreResultDto>>(genre);
            return new PaginatedResponse<GetGenreResultDto>()
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                Items = result.ToPageSize(request.PageIndex, request.PageSize),
                TotalRecords = result.Count
            };
        }

        public async Task<BaseResponse<GetGenreResultDto>> GetGenre(Guid id)
        {
            if (!await context.Genres.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetGenreResultDto>("Genre not found", false, null);
            }

            var genre = await context.Genres.Include(x => x.Movies).ThenInclude(c => c.Movie).FirstOrDefaultAsync(x => x.Id == id);
            var result = mapper.Map<GetGenreResultDto>(genre);
            return new BaseResponse<GetGenreResultDto>("Genre retrieved", true, result);
        }

        public async Task<BaseResponse<GetGenreResultDto>> UpdateGenre(Guid id, GenreDto model)
        {
            if (!await context.Genres.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetGenreResultDto>("Genre not found", false, null);
            }

            var genreInDb = await context.Genres.FirstOrDefaultAsync(x => x.Id.Equals(id));
            var genreUpdate = mapper.Map(model, genreInDb);
            context.Genres.Attach(genreUpdate);
            await context.SaveChangesAsync(default);
            return new BaseResponse<GetGenreResultDto>("Update successful", true, null);
        }

        public async Task<BaseResponse<GetMovieResultDto>> DeleteGenre(Guid id)
        {
            if (!await context.Genres.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetMovieResultDto>("Genre not found", false, null);
            }

            var movie = await context.Genres.FirstOrDefaultAsync(x => x.Id.Equals(id));
            movie.IsDeleted = true;
            movie.DeletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(default);
            return new BaseResponse<GetMovieResultDto>("Deleted successfully", true, null);
        }
    }
}
