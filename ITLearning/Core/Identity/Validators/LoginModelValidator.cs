using FluentValidation;
using ITLearning.Frontend.Web.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login jest wymagany.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Hasło jest wymagane.");
        }
    }
}
