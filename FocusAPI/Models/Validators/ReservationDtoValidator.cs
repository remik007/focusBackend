using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class ReservationDtoValidator : AbstractValidator<ReservationDto>
    {
        public ReservationDtoValidator(FocusDbContext dbContext)
        {
            RuleFor(x => x.OwnerId)
                .NotEmpty();
            RuleFor(x => x.TripId)
                .NotEmpty();
        }
    }
}
