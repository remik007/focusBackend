using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TripDtoValidator : AbstractValidator<TripDto>
    {
        public TripDtoValidator()
        {
            RuleFor(x => x.ShortName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.ShortDescription)
               .MaximumLength(2047);
            RuleFor(x => x.Description)
               .MaximumLength(2147483647); //max text length in SQLITE 2147483647
            RuleFor(x => x.Prize)
               .MaximumLength(32);
            RuleFor(x => x.OldPrize)
               .MaximumLength(32);
            RuleFor(x => x.AvailableSeats)
                .NotEmpty();
            RuleFor(x => x.From)
                .NotEmpty();
            RuleFor(x => x.To)
                .NotEmpty();
            RuleFor(x => x.ImageUrl)
              .MaximumLength(1023);
        }
    }
}