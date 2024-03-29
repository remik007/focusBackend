﻿using FluentValidation;
using FocusAPI.Data;

namespace FocusAPI.Models.Validators
{
    public class SubPageDtoValidator : AbstractValidator<SubPageDto>
    {
        public SubPageDtoValidator()
        {
            RuleFor(x => x.ShortName)
                .NotEmpty()
                .MaximumLength(63);
            RuleFor(x => x.ShortDescription)
                .MaximumLength(2047);
            RuleFor(x => x.Description)
                .MaximumLength(20479);
            RuleFor(x => x.ImageName)
                .MaximumLength(1023);
        }
    }
}