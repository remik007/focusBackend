using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TripCategoryDtoValidator : AbstractValidator<TripCategoryDto>
    {
        public TripCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}