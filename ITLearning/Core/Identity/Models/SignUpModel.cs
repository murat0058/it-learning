using FluentValidation.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Models
{
    //TODO rename and move to viewmodels or data contracts
    [Validator(typeof(SignUpModelValidator))]
    public class SignUpModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
