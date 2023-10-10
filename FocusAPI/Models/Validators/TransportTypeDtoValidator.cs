using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TransportTypeDtoValidator : AbstractValidator<TransportTypeDto>
    {
        public TransportTypeDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}