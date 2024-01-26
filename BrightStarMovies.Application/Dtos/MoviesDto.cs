using BrightStarMovies.Domain.Entities;

namespace BrightStarMovies.Application.Dtos
{
    public class GetMovieResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string Country { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public string PhotoUrl { get; set; }
    }

    public class MovieDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }

    }

    public class MovieGenreDto
    {
        public Guid GenreId { get; set; }
        public GenreDto Genre { get; set; }
        public Guid MovieId { get; set; }
        public MovieDto Movie { get; set; }
    }

}
