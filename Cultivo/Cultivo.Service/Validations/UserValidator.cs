using Cultivo.Domain.Constants;
using Cultivo.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cultivo.Service.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage(Validators.MESSAGE_NAME_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_NAME_NULL);

            RuleFor(model => model.LastName)
                .NotEmpty().WithMessage(Validators.MESSAGE_LASTNAME_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_LASTNAME_NULL);

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage(Validators.MESSAGE_EMAIL_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_EMAIL_NULL);

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage(Validators.MESSAGE_PASSWORD_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_PASSWORD_NULL);
        }
    }
}
