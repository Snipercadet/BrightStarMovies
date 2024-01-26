using BrightStarMovies.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Domain.Entities
{
    public class Movie  : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public int Rating { get; set; }
        public decimal TicketPrice { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<MovieGenre> Genres { get; set; }
    }
}
