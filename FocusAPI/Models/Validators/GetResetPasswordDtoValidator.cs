using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class GetResetPasswordDtoValidator : AbstractValidator<GetResetPasswordDto>
    {
        private readonly FocusDbContext _context;
        public GetResetPasswordDtoValidator(FocusDbContext context)
        {
            _context = context;
            RuleFor(x => x.Login)
                .NotEmpty()
                .Custom((value, context) =>
                 {
                     var loginInUse = _context.AppUsers.Any(u => u.Email == value || u.UserName == value);
                     if (!loginInUse)
                         context.AddFailure("Login", "This account does not exist");
                 });
        }
    }
}
