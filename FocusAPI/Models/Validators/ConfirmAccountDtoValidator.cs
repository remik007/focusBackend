using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class ConfirmAccountDtoValidator : AbstractValidator<ConfirmAccountDto>
    {
        private readonly AppSettings _appSettings;
        public ConfirmAccountDtoValidator(AppSettings appSettings)
        {
            _appSettings = appSettings;

            RuleFor(x => x.Login)
                .NotEmpty();

            RuleFor(x => x.Token)
                .NotEmpty()
                .Length(_appSettings.AccountTokenLength);
            
        }
    }
}
