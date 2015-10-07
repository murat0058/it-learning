using FluentValidation;
using ITLearning.Frontend.Web.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Validators
{
    public class SignUpModelValidator : AbstractValidator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login jest wymagany.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Adres e-mail jest wymagany.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Hasło jest wymagane.");

            RuleFor(x => x.PasswordConfirmation)
                .NotEmpty()
                .WithMessage("Potwierdzenie hasła jest wymagane.");

            RuleFor(x => x.Password)
                .Equal(x => x.PasswordConfirmation)
                .When(x => !string.IsNullOrEmpty(x.Password) && !string.IsNullOrEmpty(x.PasswordConfirmation))
                .WithMessage("Wpisane hasła nie pasują do siebie.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("To nie jest poprawny adres e-mail");
        }
    }
}
