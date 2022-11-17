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
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty().WithMessage(Validators.MESSAGE_NAME_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_NAME_NULL);

            RuleFor(model => model.written)
                .NotEmpty().WithMessage(Validators.MESSAGE_LASTNAME_EMPTY)
                .NotNull().WithMessage(Validators.MESSAGE_LASTNAME_NULL);
        }
    }
}
