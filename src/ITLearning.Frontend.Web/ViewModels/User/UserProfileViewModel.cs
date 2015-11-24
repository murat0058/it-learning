using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.User
{
    public class UserProfileViewModel
    {
        [Display(Name="Imię")]
        [MaxLength(50, ErrorMessage = "Podane imię jest zbyt długie (max. 50 znaków).")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Podane nazwisko jest zbyt długie (max. 50 znaków).")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail jest wymagany.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format adresu e-mail jest niepoprawny.")]
        public string Email { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        public string ProfileImagePath { get; set; }
    }
}