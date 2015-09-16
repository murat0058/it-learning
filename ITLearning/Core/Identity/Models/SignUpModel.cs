using FluentValidation.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Models
{
    [Validator(typeof(SignUpModelValidator))]
    public class SignUpModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
