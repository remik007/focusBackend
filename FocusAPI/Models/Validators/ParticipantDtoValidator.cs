using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class ParticipantDtoValidator : AbstractValidator<ParticipantDto>
    {
        public ParticipantDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(x => x.ReservationId)
                .NotEmpty();
            RuleFor(x => x.Birthday)
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .MaximumLength(16);
            RuleFor(x => x.DocumentNumber)
                .MaximumLength(255);
        }
    }
}
