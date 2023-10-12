using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TripCategoryDtoValidator : AbstractValidator<TripCategoryDto>
    {
        public TripCategoryDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}