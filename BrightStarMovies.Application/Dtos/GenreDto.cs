using BrightStarMovies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Application.Dtos
{
    public class GetGenreResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MovieDto> Movies { get; set; }
    }

    public class GenreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
