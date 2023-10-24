using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        private readonly AppSettings _appSettings;
        public ResetPasswordDtoValidator(AppSettings appSettings)
        {
            _appSettings = appSettings;

            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Token)
                .NotEmpty()
                .Length(_appSettings.AccountTokenLength);
            
        }
    }
}
