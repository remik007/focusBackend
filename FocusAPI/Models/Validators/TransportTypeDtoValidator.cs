using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class TransportTypeDtoValidator : AbstractValidator<TransportTypeDto>
    {
        public TransportTypeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}