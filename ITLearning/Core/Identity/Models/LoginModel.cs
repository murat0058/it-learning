using FluentValidation.Attributes;
using ITLearning.Frontend.Web.Core.Identity.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Core.Identity.Models
{
    [Validator(typeof(LoginModelValidator))]
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
