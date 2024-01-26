using BrightStarMovies.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Domain.Entities
{
    public class MovieGenre :  BaseEntity
    {
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
