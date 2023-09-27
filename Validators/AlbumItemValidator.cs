using System;
using FluentValidation;
using RankingApp.Models;

namespace RankingApp.Validators
{
    public class AlbumItemValidator : AbstractValidator<AlbumItemModel>
    {
        public AlbumItemValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Album Title cannot be empty")
                .NotNull().WithMessage("Album Title cannot be null");

            RuleFor(x => x.Ranking)
                .NotNull().WithMessage("Ranking cannot be null")
                .Must(ranking => !(ranking is string)).WithMessage("Ranking cannot be a string");
        }
    }
}