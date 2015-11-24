using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login jest wymagany.")]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}
