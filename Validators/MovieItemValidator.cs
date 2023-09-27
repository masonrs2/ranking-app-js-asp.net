using System;
using FluentValidation;
using RankingApp.Models;

namespace RankingApp.Validators
{
    public class MovieItemValidator : AbstractValidator<MovieItemModel>
    {
        public MovieItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty")
                .NotNull().WithMessage("Title cannot be null");

            RuleFor(x => x.Ranking)
                .NotNull().WithMessage("Ranking cannot be null")
                .Must(ranking => !(ranking is string)).WithMessage("Ranking cannot be a string");
        }
    }
}