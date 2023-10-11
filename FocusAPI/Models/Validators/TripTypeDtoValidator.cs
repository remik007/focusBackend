using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TripTypeDtoValidator : AbstractValidator<TripCategoryDto>
    {
        public TripTypeDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}