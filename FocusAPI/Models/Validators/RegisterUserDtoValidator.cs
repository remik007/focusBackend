using FluentValidation;
using FocusAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FocusAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        private readonly FocusDbContext _context;
        public RegisterUserDtoValidator(FocusDbContext dbContext)
        {
            _context = dbContext;
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = _context.AppUsers.Any(u => u.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "This email address is taken");
                });

            RuleFor(x => x.UserName)
                .Custom((value, context) =>
                {
                    var usernameInUse = _context.AppUsers.Any(u => u.UserName == value);
                    if (usernameInUse)
                        context.AddFailure("UserName", "This username is taken");
                });
        }
    }
}
