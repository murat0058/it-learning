using System.ComponentModel.DataAnnotations;

namespace ITLearning.Frontend.Web.ViewModels.User
{
    public class UserProfileViewModel
    {
        [Display(Name="Imię")]
        [MaxLength(50, ErrorMessage = "Podane imię jest zbyt długie")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [MaxLength(50, ErrorMessage = "Podane nazwisko jest zbyt długie")]
        public string LastName { get; set; }

        [Display(Name = "Adres email")]
        [Required(ErrorMessage = "Adres email nie może być pusty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Adres email jest niepoprawny")]
        public string Email { get; set; }

        [Display(Name = "Login")]
        public string UserName { get; set; }

        public string ProfileImagePath { get; set; }
    }
}