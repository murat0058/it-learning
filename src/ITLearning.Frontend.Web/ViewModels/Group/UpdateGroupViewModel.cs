using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class UpdateGroupViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [MaxLength(50, ErrorMessage = "Podana nazwa jest zbyt długa (max. 50 znaków).")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Opis jest wymagany.")]
        [MaxLength(300, ErrorMessage = "Podany opis jest za długi (max. 300 znaków).")]
        public string Description { get; set; }

        [Display(Name = "Grupa prywatna")]
        public bool IsPrivate { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane.")]
        [Compare("Password", ErrorMessage = "Hasła nie pasuja do siebie.")]
        public string PasswordConfirmation { get; set; }
    }
}
