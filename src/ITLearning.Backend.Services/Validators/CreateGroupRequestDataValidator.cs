using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using ITLearning.Shared.Extensions;
using ITLearning.Contract.Data.Requests.Groups;

namespace ITLearning.Backend.Business.Validators
{
    public class CreateGroupRequestDataValidator : AbstractValidator<CreateGroupRequestData>
    {
        public CreateGroupRequestDataValidator()
        {
            RuleFor(p => p)
                .Must(HaveValidNameAndDescription)
                .WithMessage("Nazwa i opis muszą nie mogą być puste.");

            RuleFor(p => p)
                .Must(HaveCorrectPasswords)
                .When(p => p.IsPrivate)
                .WithMessage("Hasła nie pasują do siebie lub są puste.");
        }

        private bool HaveCorrectPasswords(CreateGroupRequestData data)
        {
            return data.Password.NotNullNorEmpty() && data.PasswordConfirmation.NotNullNorEmpty() 
                && data.Password == data.PasswordConfirmation;
        }

        private bool HaveValidNameAndDescription(CreateGroupRequestData data)
        {
            return data.Name.NotNullNorEmpty() && data.Description.NotNullNorEmpty();
        }
    }
}
