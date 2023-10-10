using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TripTypeDtoValidator : AbstractValidator<TripTypeDto>
    {
        public TripTypeDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}