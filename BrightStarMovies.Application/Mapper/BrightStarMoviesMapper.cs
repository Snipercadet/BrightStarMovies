using BrightStarMovies.Application.Dtos;
using BrightStarMovies.Domain.Entities;

namespace BrightStarMovies.Application.Mapper
{
    public class BrightStarMoviesMapper : Profile
    {
        public BrightStarMoviesMapper()
        {
            CreateMap<Movie, GetMovieResultDto>();

            CreateMap<MovieGenreDto, MovieDto>();
            CreateMap<Movie, MovieDto>().ReverseMap();

            CreateMap<MovieGenre, MovieGenreDto>()
                 .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
                 .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
                 .IncludeMembers(src => src.Genre);
            CreateMap<Genre, MovieGenreDto>();
            CreateMap<MovieGenre, GenreDto>();

            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre,  GetGenreResultDto>();

            CreateMap<Movie, GetMovieResultDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => new GenreDto
            {
                Name = g.Genre.Name,
                Description = g.Genre.Description
            })));

            CreateMap<Genre, GetGenreResultDto>()
            .ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.Movies.Select(g => new MovieDto
            {
                Name = g.Genre.Name,
                Description = g.Movie.Description,
                ReleaseDate = g.Movie.ReleaseDate,
                Rating = g.Movie.Rating,
                TicketPrice = g.Movie.TicketPrice,
                Country = g.Movie.Country,
                PhotoUrl = g.Movie.PhotoUrl
                
            })));
        }
    }
}
