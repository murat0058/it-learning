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
            RuleFor(x => x.Email).EmailAddress().WithMessage("This is not valid e-mail address");
        }
    }
}
