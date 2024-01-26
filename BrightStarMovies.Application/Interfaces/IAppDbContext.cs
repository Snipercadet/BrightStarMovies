using BrightStarMovies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<MovieGenre> MovieGenre { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
