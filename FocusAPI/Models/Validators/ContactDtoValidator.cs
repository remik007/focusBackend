using FluentValidation;
using FocusAPI.Data;
using System.Linq.Expressions;

namespace FocusAPI.Models.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(255);
            RuleFor(x => x.AddressLine1)
                .MaximumLength(255);
            RuleFor(x => x.AddressLine2)
                .MaximumLength(255);
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(16);
            RuleFor(x => x.Facebook)
                .MaximumLength(255);
            RuleFor(x => x.Instagram)
                .MaximumLength(255);
            RuleFor(x => x.Google)
                .MaximumLength(255);
        }
    }
}