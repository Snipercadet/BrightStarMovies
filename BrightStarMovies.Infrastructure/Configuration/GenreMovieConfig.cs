using BrightStarMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BrightStarMovies.Infrastructure.Configuration
{
    public class GenreMovieConfig : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(x => new { x.GenreId, x.MovieId });
        }
    }
}
