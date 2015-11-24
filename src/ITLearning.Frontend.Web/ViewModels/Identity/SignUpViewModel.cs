using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login jest wymagany.")]
        public string Login { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail jest wymagany.")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie pasuja do siebie.")]
        public string PasswordConfirmation { get; set; }
    }
}
