using AutoMapper;
using Azure.Core;
using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Application.Interfaces;
using BrightStarMovies.Domain.Common.Extensions;
using BrightStarMovies.Domain.Common.Request;
using BrightStarMovies.Domain.Common.Response;
using BrightStarMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Infrastructure.Services
{
    public class MovieService(IAppDbContext context, IMapper mapper) : IMovieService
    {
        public async Task<BaseResponse<GetMovieResultDto>> AddMovies(MovieDto request)
        {
            if (await context.Movies.AnyAsync(x => x.Name.Equals(request.Name)))
            {
                return new BaseResponse<GetMovieResultDto>("A movie already exist with this title", false, null);
            }

            var movie = mapper.Map<Movie>(request);
            context.Movies.Add(movie);
            await context.SaveChangesAsync(default);
            var result = mapper.Map<GetMovieResultDto>(movie);
            return new BaseResponse<GetMovieResultDto>("Movie created successfully", true, result);
        }

        public async Task<PaginatedResponse<GetMovieResultDto>> GetMovies(PaginationData request)
        {
            var movies = await context.Movies.ToListAsync();
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                movies = movies.Where(x => x.Name == request.SearchTerm).ToList();
            }

            if (request.FromDate.HasValue)
            {
                movies = movies.FilterFromDate(request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                movies = movies.FilterToDate(request.ToDate.Value);
            }

            var result = mapper.Map<List<GetMovieResultDto>>(movies);
            return new PaginatedResponse<GetMovieResultDto>()
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                Items = result.ToPageSize(request.PageIndex, request.PageSize),
                TotalRecords = result.Count
            };
        }

        public async Task<BaseResponse<GetMovieResultDto>> GetMovie(Guid id)
        {
            if(!await context.Movies.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetMovieResultDto>("Movie not found", false, null);
            }

            var movie = await context.Movies.Include(x=>x.Genres).ThenInclude(c=>c.Genre).FirstOrDefaultAsync(x=>x.Id == id);
            var result = mapper.Map<GetMovieResultDto>(movie);
            return new BaseResponse<GetMovieResultDto>("Movie retrieved", true, result);
        }

        public async Task<BaseResponse<GetMovieResultDto>> UpdateMovie(Guid id, MovieDto model)
        {
            if(!await context.Movies.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetMovieResultDto>("Movie not found", false, null);
            }

            var movieInDb = await context.Movies.FirstOrDefaultAsync(x => x.Id.Equals(id));
            var movie = mapper.Map(model, movieInDb);
            context.Movies.Attach(movie);
            await context.SaveChangesAsync(default);
            return new BaseResponse<GetMovieResultDto>("Update successful", true, null);
        }

        public async Task<BaseResponse<GetMovieResultDto>> DeleteMovie(Guid id)
        {
            if (!await context.Movies.AnyAsync(x => x.Id.Equals(id)))
            {
                return new BaseResponse<GetMovieResultDto>("Movie not found", false, null);
            }

            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
            movie.IsDeleted = true;
            movie.DeletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(default);
            return new BaseResponse<GetMovieResultDto>("Deleted successfully", true, null);
        }

        public async Task<BaseResponse<GetMovieResultDto>> AddMovieToGenre(Guid movieId, List<Guid> genreIds)
        {
            var movieGenre = new List<MovieGenre>();
            if (!await context.Movies.AnyAsync(x => x.Id.Equals(movieId)))
            {
                return new BaseResponse<GetMovieResultDto>("Movie not found", false, null);
            }
            foreach (var id in genreIds)
            {
                if (!await context.Genres.AnyAsync(x => x.Id == id))
                {
                    return new BaseResponse<GetMovieResultDto>($"Genre with id:{id} not found", false, null);
                }
                else
                {
                    var newMovieGenre = new MovieGenre { GenreId = id, MovieId = movieId, Id = Guid.NewGuid() };
                    movieGenre.Add(newMovieGenre);
                }
            }
            await context.MovieGenre.AddRangeAsync(movieGenre);
            await context.SaveChangesAsync(default);

            return new BaseResponse<GetMovieResultDto>("Updated movie genre", true, null);
         
        }

        public async Task<BaseResponse<GetMovieResultDto>> RemoveMovieFromGenre(Guid movieId, List<Guid> genreIds)
        {
            var movieGenre = new List<MovieGenre>();
            if (!await context.Movies.AnyAsync(x => x.Id.Equals(movieId)))
            {
                return new BaseResponse<GetMovieResultDto>("Movie not found", false, null);
            }
            foreach (var id in genreIds)
            {
                if (!await context.Genres.AnyAsync(x => x.Id == id))
                {
                    return new BaseResponse<GetMovieResultDto>($"Genre with id:{id} not found", false, null);
                }
                else
                {
                    var newMovieGenre = await context.MovieGenre.Where(x => x.MovieId.Equals(movieId) && x.GenreId.Equals(id)).FirstOrDefaultAsync();
                    movieGenre.Add(newMovieGenre);
                }
            }
            context.MovieGenre.RemoveRange(movieGenre);
            await context.SaveChangesAsync(default);

            return new BaseResponse<GetMovieResultDto>("Removed movie from genre", true, null);

        }
    }
}
