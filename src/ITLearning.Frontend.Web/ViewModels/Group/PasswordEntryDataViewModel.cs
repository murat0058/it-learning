using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.ViewModels.Group
{
    public class PasswordEntryDataViewModel
    {
        public int GroupId { get; set; }

        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        public string Password { get; set; }
    }
}
