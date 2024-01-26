using BrightStarMovies.Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightStarMovies.Application.Validator
{
    public class MovieValidator : AbstractValidator<MovieDto>
    {
        public MovieValidator()
        {
            RuleFor(x => x.Rating).GreaterThan(0).LessThanOrEqualTo(5);
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.ReleaseDate).NotNull().NotEmpty();
            RuleFor(x => x.TicketPrice).NotNull().NotEmpty();
            RuleFor(x => x.Country).NotNull().NotEmpty();
            RuleFor(x => x.PhotoUrl).NotNull().NotEmpty();
        }
    }
}
